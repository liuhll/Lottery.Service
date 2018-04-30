using System;
using System.ComponentModel;
using System.Linq;
using Dapper;
using ECommon.Components;
using ECommon.Dapper;
using ECommon.Extensions;
using Lottery.Core.Caching;
using Lottery.Dtos.Account;
using Lottery.Dtos.AuthRanks;
using Lottery.Dtos.Auths;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Menbers;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.Tools;
using Lottery.QueryServices.Goods;

namespace Lottery.QueryServices.Dapper.Goods
{
    [Component]
    public class SellCallBackService : BaseQueryService, ISellCallBackService
    {
        private readonly ICacheManager _cacheManager;

        public SellCallBackService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void PayCallBack(NotifyCallBackInput input, UserBaseDto userInfo, out string lotteryId)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        #region 支付处理

                        var orderDto = conn.QueryList<OrderInfoInfo>(new { SalesOrderNo = input.Orderid },
                            TableNameConstants.OrderdRecordTable, transaction: transaction).First();
                        var authRank = conn
                            .QueryList<AuthRankDto>(new {Id = orderDto.AuthRankId}, TableNameConstants.AuthRankTable, transaction: transaction)
                            .First();

                        var authinfoLast = conn.QueryList<AuthOrderInfo>(new
                        {
                            AuthUserId = userInfo.Id,
                            LotteryId = orderDto.LotteryId,
                            Status = AuthStatus.Normal
                        }, TableNameConstants.AuthorizeRecordTable, transaction: transaction).OrderByDescending(p=>p.CreateTime).FirstOrDefault();

                        var memberInfo = conn
                            .QueryList<MemberInfoDto>(new
                            {
                                UserId = userInfo.Id,
                                LotteryId = orderDto.LotteryId,
                            }, TableNameConstants.MemberTable, transaction: transaction)
                            .FirstOrDefault();


                        orderDto.PayCost = input.Realprice;
                        orderDto.Status = PayStatus.Payed;
                        orderDto.Payer = userInfo.Id;
                        orderDto.ThirdPayOrderNo = input.Paysapi_id;
                        orderDto.PayOrderNo = OrderHelper.GenerateOrderNo(OrderType.Pay, SellType.Rmb);
                        orderDto.PayType = PayType.PaysApi;
                        orderDto.PayTime = DateTime.Now;
                      
                        conn.Update(orderDto, new {orderDto.Id}, TableNameConstants.OrderdRecordTable,
                            transaction: transaction);

                        var authinfo = new AuthOrderInfo()
                        {
                            Id = Guid.NewGuid().ToString(),
                            AuthRankId = orderDto.AuthRankId,
                            AuthUserId = userInfo.Id,
                            AuthOrderNo = OrderHelper.GenerateOrderNo(OrderType.Order, SellType.Rmb),
                          //  InvalidDate = DateTime.Now.AddMonths(orderDto.Count),
                            AuthTime = DateTime.Now,
                            AuthType = SellType.Rmb,
                            CreateBy = userInfo.Id,
                            CreateTime = DateTime.Now,
                            Notes = authRank.MemberRank.GetChineseDescribe(),
                            Status = AuthStatus.Normal,
                            SaleRecordId = orderDto.Id,
                            LotteryId = orderDto.LotteryId
                        };

                        if (authinfoLast == null)
                        {
                            authinfo.InvalidDate = DateTime.Now.AddMonths(orderDto.Count);
                        }
                        else
                        {
                            authinfo.InvalidDate = authinfoLast.InvalidDate.AddMonths(orderDto.Count);

                            authinfoLast.UpdateBy = userInfo.Id;
                            authinfoLast.UpdateTime = DateTime.Now;
                            authinfoLast.Status = AuthStatus.Invalid;
                            conn.Update(authinfoLast, new { Id = authinfoLast.Id }, TableNameConstants.AuthorizeRecordTable, transaction);

                        }
                        conn.Insert(authinfo, TableNameConstants.AuthorizeRecordTable, transaction);

                        if (memberInfo == null)
                        {
                            memberInfo = new MemberInfoDto()
                            {
                                Id = Guid.NewGuid().ToString(),
                                InvalidDate = authinfo.InvalidDate,
                                LastAuthOrderId = authinfo.Id,
                                LotteryId = orderDto.LotteryId,
                                MemberRank = (int)authRank.MemberRank,
                                Status = MemberStatus.Normal,
                                UserId = userInfo.Id,
                                CreateBy = userInfo.Id,
                                CreateTime = DateTime.Now
                            };
                            conn.Insert(memberInfo, TableNameConstants.MemberTable, transaction);
                        }
                        else
                        {
                            memberInfo.MemberRank = (int) authRank.MemberRank;
                            memberInfo.InvalidDate = authinfo.InvalidDate;
                            memberInfo.UpdateBy = userInfo.Id;
                            memberInfo.UpdateTime = DateTime.Now;
                            conn.Update(memberInfo, new { Id = memberInfo.Id }, TableNameConstants.MemberTable, transaction);
                        }

                        #endregion 支付处理

                        #region 指标配置

                        //var userNormConigs = conn.QueryList<NormConfigDto>(new { UserId = userInfo.Id, IsEnable = 1 }, TableNameConstants.NormConfigTable, transaction: transaction);

                        //if (!userNormConigs.Safe().Any())
                        //{
                        //    var normConfigs = conn.QueryList<NormConfigDto>(new { LotteryId = orderDto.LotteryId, IsDefualt = 1, IsEnable = 1 }, TableNameConstants.NormConfigTable, transaction: transaction);

                        //    foreach (var normConfig in normConfigs)
                        //    {
                        //        normConfig.UserId = userInfo.Id;
                        //        normConfig.Id = Guid.NewGuid().ToString();
                        //        conn.Insert(normConfig, TableNameConstants.NormConfigTable, transaction);
                        //    }
                        //}

                        #endregion 指标配置

                        #region 清除相关缓存

                        var redisKey1 = string.Format(RedisKeyConstants.MEMBERRANK_MEMBERPOWER_KEY, orderDto.LotteryId,
                            authRank.MemberRank);
                        var redisKey2 = string.Format(RedisKeyConstants.MEMBERRANK_ROLE_KEY, orderDto.LotteryId,
                            authRank.MemberRank);
                        var redisKey3 = string.Format(RedisKeyConstants.USERINFO_KEY, userInfo.Id);
                        var redisKey4 = string.Format(RedisKeyConstants.OPERATION_MEMBERINFO_KEY, orderDto.LotteryId);
                        _cacheManager.RemoveByPattern(redisKey1);
                        _cacheManager.RemoveByPattern(redisKey2);
                        _cacheManager.RemoveByPattern(redisKey3);
                        _cacheManager.RemoveByPattern(redisKey4);
                        transaction.Commit();

                        lotteryId = orderDto.LotteryId;

                        #endregion 清除相关缓存
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }
        }
    }
}