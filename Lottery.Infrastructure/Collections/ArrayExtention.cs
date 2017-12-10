namespace Lottery.Infrastructure.Collections
{
    public static class ArrayExtention
    {
        public static T[,] ConvertToArray<T>(this T[][] data)
        {
            T[,] res = new T[data.Length, data[0].Length];
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++) res[i, j] = data[i][j];
            }
            return res;
        }
    }
}