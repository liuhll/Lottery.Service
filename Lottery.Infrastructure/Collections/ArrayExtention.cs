using System;
using System.Linq;

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

        public static int IndexOf<T>(this T[] source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            int index = -1;

            int step = 0;

            foreach (var k in source)
            {
                if (k.Equals(item))
                {
                    index = step;
                    break;
                }
                step++;
            }

            if (index < 0)
            {
                throw new Exception($"该数组不包含{item}元素");
            }
            return index;
        }
    }
}