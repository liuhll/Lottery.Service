namespace Lottery.Dtos.PageList
{
    public interface IPageList<T> where T : class
    {
        int PageSize { get; }

        int TotalCount { get; }

        int PageIndex { get; }

        int PageCount { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}