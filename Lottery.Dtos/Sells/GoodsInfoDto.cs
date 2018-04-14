namespace Lottery.Dtos.Sells
{
    public class GoodsInfoDto
    {
        public string Id { get; set; }

        public string AuthRankId { get; set; }
        public string GoodName { get; set; }

        public int? Term { get; set; }

        public int MemberRank { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

       // public double Discount { get; set; } = 1.00;
    }
}