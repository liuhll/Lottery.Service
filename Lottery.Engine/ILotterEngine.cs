namespace Lottery.Engine
{
    public interface ILotterEngine
    {
        object[] Perdictor(object config);


        bool GetPerdictResult();

        object DataAnalyse();

    }
}
