namespace OrderService.Application.Common.Pagination;

public class PaginatedResult<T>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public IEnumerable<T> Items { get; }

    public PaginatedResult(int pageNumber, int pageSize, int totalCount, IEnumerable<T> items)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }
}
