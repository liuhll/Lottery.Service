﻿using System.Collections.Generic;
using System.Diagnostics;
using ECommon.Components;
using Lottery.Dtos.Auths;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.QueryServices.Activities;
using Lottery.QueryServices.Goods;

namespace Lottery.AppService.Sell
{
    [Component]
    public class SellAppService : ISellAppService
    {
        private readonly ISellQueryService _sellQueryService;
        private readonly ILotterySession _lotterySession;
        private readonly IActivityQueryService _activityQueryService;

        public SellAppService(ISellQueryService sellQueryService,
            IActivityQueryService activityQueryService)
        {
            _sellQueryService = sellQueryService;
            _activityQueryService = activityQueryService;
            _lotterySession = NullLotterySession.Instance;
        }

        public ICollection<SellTypeOutput> GetSalesType(MemberRank memberRank)
        {
            ICollection<SellTypeOutput> result = new List<SellTypeOutput>();
            switch (memberRank)
            {
                case MemberRank.Ordinary:
                case MemberRank.Senior:
                    result.Add(new SellTypeOutput()
                    {
                        SellType = SellType.Point,
                        SellTypeName = "积分兑换"
                    });
                    result.Add(new SellTypeOutput()
                    {
                        SellType = SellType.Rmb,
                        SellTypeName = "购买"
                    });
                    break;
                case MemberRank.Specialty:
                case MemberRank.Team:
                    result.Add(new SellTypeOutput()
                    {
                        SellType = SellType.Rmb,
                        SellTypeName = "购买"
                    });
                    break;
            }
            return result;
        }


        public ICollection<GoodsOutput> GetGoodsInfos(MemberRank memberRank, string lotteryId, SellType sellType)
        {
            IList<GoodsOutput> result;
            switch (sellType)
            {
                case SellType.Point:
                    result = GetPointGoodInfos(memberRank, lotteryId);
                    break;
                case SellType.Rmb:
                    result = GetRmbGoodInfos(memberRank, lotteryId);
                    break;
                default:
                    throw new LotteryException("参数错误，不存在该销售类型");
            }
            return result;
        }

        private IList<GoodsOutput> GetRmbGoodInfos(MemberRank memberRank, string lotteryId)
        {
            var result = new List<GoodsOutput>();
            var goodOutputs = _sellQueryService.GetRmbGoodInfos(memberRank,lotteryId);
            var userAuthInfo = _sellQueryService.GetUserAuthInfo(_lotterySession.UserId,lotteryId);
            foreach (var goods in goodOutputs)
            {
                Debug.Assert(goods.Term.HasValue);
                var output = new GoodsOutput()
                {
                    GoodsName = goods.GoodName,
                    Count = goods.Term.Value,
                    Discount = GetDiscount(goods.AuthRankId,SellType.Rmb),
                    PurchaseType = GetPurchaseType(userAuthInfo,goods.MemberRank),
                    OriginalPrice = goods.Price * goods.Term.Value
                };
                result.Add(output);
            }
            return result;
        }

        private PurchaseType GetPurchaseType(UserAuthDto userAuthInfo,int memberRank)
        {
            if (userAuthInfo == null)
            {
                return PurchaseType.New;
            }
            else if ((int) _lotterySession.MemberRank == memberRank)
            {
                return PurchaseType.Continuation;
            }
            else
            {
                return PurchaseType.Upgrade;
            }
        }

        private double GetDiscount(string authRankId,SellType sellType)
        {
            var activity = _activityQueryService.GetAuthAcivity(authRankId,sellType);
            if (activity == null)
            {
                return 1.00;
            }
            return activity.Discount;
        }

        private IList<GoodsOutput> GetPointGoodInfos(MemberRank memberRank, string lotteryId)
        {
            var result = new List<GoodsOutput>();
            var goodOutputs = _sellQueryService.GetPointGoodInfos(memberRank, lotteryId);
            var userAuthInfo = _sellQueryService.GetUserAuthInfo(_lotterySession.UserId, lotteryId);
            foreach (var goods in goodOutputs)
            {
                var output = new GoodsOutput()
                {
                    GoodsName = goods.GoodName,
                    Count = 1,
                    Discount = GetDiscount(goods.AuthRankId, SellType.Point),
                    PurchaseType = GetPurchaseType(userAuthInfo, goods.MemberRank),
                    OriginalPrice = goods.Price * 1
                };
                result.Add(output);
            }
            return result;
        }
    }
}