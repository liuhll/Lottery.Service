using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Dtos.PageList
{
    public class PageList<T> : IPageList<T> where T: class 
    {
        public PageList(IEnumerable<T> list,int pageIndex = 1, int pageSize = 20)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalCount = list.Count();
            PageCount = (int)Math.Ceiling((decimal)TotalCount / PageSize);
            Data = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public int PageSize { get; }
        public int TotalCount { get; }
        public int PageIndex { get; }
        public int PageCount { get; }

        public ICollection<T> Data { get; }

        public bool HasPreviousPage {
            get { return PageIndex > 1; }
        }
        public bool HasNextPage {
            get { return PageIndex < PageCount; }
        }
    }
}