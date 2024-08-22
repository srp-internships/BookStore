namespace OrderService.Application.Common.Pagination;

public record PagingParameters
{
    private int _page;
    private int _pageSize;

    public bool? OrderByDescending { get; set; } = true;

    public int? PageNumber
    {
        get => _page;
        set => _page = Math.Clamp(value ?? 1, 1, int.MaxValue);
    }

    public int? PageSize
    {
        get => _pageSize == 0 ? PagingConstants.DefaultPageSize : _pageSize;
        set => _pageSize = Math.Clamp(value ?? 10, 1, PagingConstants.MaxPageSize);
    }

    public int Skip => ((int)PageNumber - 1) * (int)PageSize;
    public int Take => (int)PageSize;
}
