using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Auths;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Goods;

namespace Lottery.QueryServices.Dapper.Goods
{
    [Component]
    public class SellQueryService :BaseQueryService, ISellQueryService
    {
        private readonly ICacheManager _cacheManager;

        public SellQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }


        public IList<GoodsInfoDto> GetRmbGoodInfos(MemberRank memberRank, string lotteryId)
        {
            return GetRmbGoodInfos(lotteryId).Where(p => p.MemberRank >= (int) memberRank).ToList();
        }

        private IList<GoodsInfoDto> GetRmbGoodInfos(string lotteryId)
        {
            var cacheKey = string.Format(RedisKeyConstants.LOTTERY_GOODS_LIST, "RMB", lotteryId);
            return _cacheManager.Get<IList<GoodsInfoDto>>(cacheKey, () =>
            {
                // AND B.MemberRank>=@MemberRank
                using (var conn = GetLotteryConnection())
                {
                    var sql = @"SELECT A.Id,A.GoodName,A.Term,A.AuthRankId,B.MemberRank,B.Title,B.PointPrice AS Price  FROM [dbo].[S_GoodInfo] AS A
LEFT JOIN dbo.MS_AuthRank AS B ON b.Id=A.AuthRankId 
LEFT JOIN dbo.L_LotteryInfo AS C ON C.Id=B.LotteryId
WHERE B.CanSell = 1 AND A.SellType=0 
AND B.LotteryId=@LotteryId";

                    return conn.Query<GoodsInfoDto>(sql, new
                    {
                        LotteryId = lotteryId
                    }).ToList();
                }
                
            });
        }

        public IList<GoodsInfoDto> GetPointGoodInfos(MemberRank memberRank, string lotteryId)
        {
            return GetPointGoodInfos(lotteryId).Where(p => p.MemberRank >= (int) memberRank).ToList();
        }

        public UserAuthDto GetUserAuthInfo(string userId, string lotteryId)
        {
            using (var conn = GetLotteryConnection())
            {
                var sql = @"SELECT *
  FROM  [dbo].[S_AuthorizeRecord]
  WHERE InvalidDate <  GETDATE() AND Status=0
  AND LotteryId=@LotteryId
  AND AuthUserId=@UserId";

                return conn.Query<UserAuthDto>(sql, new
                {
                    LotteryId = lotteryId,
                    UserId = userId
                }).FirstOrDefault();
            }
        }

        public UserAuthOutput GetMyselfAuthInfo(string userId, string lotteryId)
        {
            using (var conn = GetLotteryConnection())
            {
                var sql = @"SELECT *,SalesOrderNo
  FROM  [dbo].[S_AuthorizeRecord] AS A
  LEFT JOIN dbo.S_OrderRecord AS B
  ON B.Id = A.SaleRecordId
  WHERE InvalidDate <  GETDATE() AND A.Status=0
  AND A.LotteryId=@LotteryId
  AND A.AuthUserId=@UserId";

                return conn.Query<UserAuthOutput>(sql, new
                {
                    LotteryId = lotteryId,
                    UserId = userId
                }).FirstOrDefault();
            }
        }

        public GoodsInfoDto GetGoodsInfoById(string goodId)
        {
            using (var conn = GetLotteryConnection())
            {
                var sql =
                    @"SELECT A.Id, A.GoodName,A.Term,A.AuthRankId,B.MemberRank,B.Title,B.PointPrice AS Price  FROM [dbo].[S_GoodInfo] AS A
LEFT JOIN dbo.MS_AuthRank AS B ON B.Id=A.AuthRankId 
LEFT JOIN dbo.L_LotteryInfo AS C ON C.Id=B.LotteryId
WHERE A.Id=@Id";

                return conn.Query<GoodsInfoDto>(sql, new
                {
                    Id = goodId
                }).FirstOrDefault();
            }
        }

        private IList<GoodsInfoDto> GetPointGoodInfos(string lotteryId)
        {
            var cacheKey = string.Format(RedisKeyConstants.LOTTERY_GOODS_LIST, "Point", lotteryId);
            return _cacheManager.Get<IList<GoodsInfoDto>>(cacheKey, () =>
            {
                // AND B.MemberRank>=@MemberRank
                using (var conn = GetLotteryConnection())
                {
                    var sql =
                        @"SELECT A.Id, A.GoodName,A.Term,A.AuthRankId,B.MemberRank,B.Title,B.PointPrice AS Price  FROM [dbo].[S_GoodInfo] AS A
LEFT JOIN dbo.MS_AuthRank AS B ON b.Id=A.AuthRankId 
LEFT JOIN dbo.L_LotteryInfo AS C ON C.Id=B.LotteryId
WHERE B.CanSell = 1 AND B.EnablePointConsume =1
AND A.SellType=1 
AND B.LotteryId=@LotteryId";

                    return conn.Query<GoodsInfoDto>(sql, new
                    {
                        LotteryId = lotteryId
                    }).ToList();
                }

            });
        }
    }
}
