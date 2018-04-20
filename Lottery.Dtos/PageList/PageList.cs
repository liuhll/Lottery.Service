using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Dtos.PageList
{
    public class PageList<T, TKey> : IPageList<T> where T : class
    {
        public PageList(IEnumerable<T> list, int pageIndex = 1, int pageSize = 20, Func<T, TKey> func = null, string order = "asc")
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalCount = list.Count();
            PageCount = (int)Math.Ceiling((decimal)TotalCount / PageSize);
            if (func != null)
            {
                switch (order)
                {
                    case "asc":
                        Data = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderBy(func).ToList();
                        break;

                    case "desc":
                        Data = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderByDescending(func).ToList();
                        break;

                    default:
                        Data = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderBy(func).ToList();
                        break;
                }
            }
            else
            {
                Data = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int PageSize { get; }
        public int TotalCount { get; }
        public int PageIndex { get; }
        public int PageCount { get; }

        public ICollection<T> Data { get; }

        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < PageCount; }
        }
    }

    public class DefaultPageList<T> : PageList<T, string> where T : class
    {
        public DefaultPageList(IEnumerable<T> list, int pageIndex = 1, int pageSize = 20, Func<T, string> func = null, string order = "asc")
            : base(list, pageIndex, pageSize, func, order)
        {
        }
    }
}