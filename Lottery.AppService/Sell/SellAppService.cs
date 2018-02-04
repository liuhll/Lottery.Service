using System.Collections.Generic;
using ECommon.Components;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Sell
{
    [Component]
    public class SellAppService : ISellAppService
    {
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

        public ICollection<GoodInfoDto> GetGoodInfos(string userId, MemberRank memberRank, SellType sellType)
        {
            throw new System.NotImplementedException();
        }
    }
}