using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Engine.LotteryData
{
    public class LotteryDataList : ILotteryDataList
    {
        private readonly IDictionary<int, ILotteryNumber> _lotteryNumbers;

        public LotteryDataList(ICollection<LotteryDataDto> lotteryDatas)
        {
            _lotteryNumbers = new Dictionary<int, ILotteryNumber>();
            foreach (var lotteryData in lotteryDatas)
            {
                if (_lotteryNumbers.ContainsKey(lotteryData.Period))
                {
                    continue;
                }
                _lotteryNumbers.AddIfNotContains(new KeyValuePair<int, ILotteryNumber>(lotteryData.Period, new LotteryNumber(lotteryData)));
            }
        }

        #region Dic Base Methods

        public IEnumerator<KeyValuePair<int, ILotteryNumber>> GetEnumerator()
        {
            return _lotteryNumbers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<int, ILotteryNumber> item)
        {
            if (!_lotteryNumbers.Contains(item))
            {
                _lotteryNumbers.Add(item);
            }
        }

        public void Clear()
        {
            _lotteryNumbers.Clear();
        }

        public bool Contains(KeyValuePair<int, ILotteryNumber> item)
        {
            return _lotteryNumbers.Contains(item);
        }

        public void CopyTo(KeyValuePair<int, ILotteryNumber>[] array, int arrayIndex)
        {
            _lotteryNumbers.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<int, ILotteryNumber> item)
        {
            return _lotteryNumbers.Remove(item);
        }

        public int Count
        {
            get { return _lotteryNumbers.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool ContainsKey(int key)
        {
            return _lotteryNumbers.ContainsKey(key);
        }

        public void Add(int key, ILotteryNumber value)
        {
            _lotteryNumbers.Add(key, value);
        }

        public bool Remove(int key)
        {
            return _lotteryNumbers.Remove(key);
        }

        public bool TryGetValue(int key, out ILotteryNumber value)
        {
            return _lotteryNumbers.TryGetValue(key, out value);
        }

        public ILotteryNumber this[int key]
        {
            get { return _lotteryNumbers[key]; }
            set { _lotteryNumbers[key] = value; }
        }

        public ICollection<int> Keys
        {
            get { return _lotteryNumbers.Keys; }
        }

        public ICollection<ILotteryNumber> Values
        {
            get { return _lotteryNumbers.Values; }
        }

        private static void CheckLotteryDataItem()
        {
        }

        #endregion Dic Base Methods

        public void AddLotteryData(LotteryDataDto data)
        {
            if (_lotteryNumbers.ContainsKey(data.Period))
            {
                throw new LotteryDataException("已经存在这一期的开奖数据");
            }
            _lotteryNumbers.Add(data.Period, new LotteryNumber(data));
        }

        public void RemoveLotteryData(LotteryDataDto data)
        {
            RemoveLotteryData(data.Period);
        }

        public void RemoveLotteryData(int period)
        {
            if (!_lotteryNumbers.ContainsKey(period))
            {
                throw new LotteryDataException("不存在这一期的开奖数据");
            }
            _lotteryNumbers.Remove(period);
        }

        public ICollection<int> LotteryDatas(int position, NumberType numberType = NumberType.Number)
        {
            var result = new List<int>();
            if (numberType == NumberType.Number)
            {
                foreach (var lotteryNumber in _lotteryNumbers)
                {
                    result.Add(lotteryNumber.Value[position]);
                }
            }
            else
            {
                foreach (var lotteryNumber in _lotteryNumbers)
                {
                    result.Add(lotteryNumber.Value.Datas.IndexOf(position));
                }
            }
            return result;
        }

        public ICollection<int> LotteryDatas(int position, int step)
        {
            var result = new List<int>();
            var effectiveLotteryNums = _lotteryNumbers.Values.Take(step);
            foreach (var lotteryNumber in effectiveLotteryNums)
            {
                result.Add(lotteryNumber[position]);
            }
            return result;
        }

        public ICollection<int> LotteryDatas(NumberType numberType = NumberType.Number, params int[] position)
        {
            var result = new List<int>();
            foreach (var ps in position)
            {
                result.AddRange(LotteryDatas(ps));
            }
            return result;
        }

        public ICollection<int> LotteryDatas(int step, params int[] position)
        {
            var result = new List<int>();
            foreach (var ps in position)
            {
                result.AddRange(LotteryDatas(ps, step));
            }
            return result;
        }
    }
}