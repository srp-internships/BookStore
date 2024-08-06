namespace ReviewService.Domain.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IReviewRepository Reviews { get; }
        Task<int> CompleteAsync();
    }
}
