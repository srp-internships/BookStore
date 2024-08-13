namespace OrderService.Application.Common.Pagination;

public record PagingParameters
{
    private int _page;
    private int _pageSize;

    public bool OrderByDescending { get; set; }

    public int PageNumber
    {
        get => _page;
        set => _page = Math.Clamp(value, 1, int.MaxValue);
    }

    public int PageSize
    {
        get => _pageSize == 0 ? PagingConstants.DefaultPageSize : _pageSize;
        set => _pageSize = Math.Clamp(value, 1, PagingConstants.MaxPageSize);
    }

    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}
