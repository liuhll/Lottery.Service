using AutoMapper.Attributes;

namespace Lottery.Dtos.Norms
{
   
    public class UserPlanNormOutput : UserNormDefaultConfigDto
    {
        [IgnoreMapFrom(typeof(UserNormDefaultConfigDto))]
        public string LotteryId { get; set; }
    }
}