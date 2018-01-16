namespace Lottery.Dtos.Norms
{
    public class UserNormPlanConfigInput : UserNormDefaultConfigInput
    {
        /// <summary>
        /// 指标Id
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 用户筛选的数据
        /// </summary>
        public string CustomNumbers { get; set; }
    }
}