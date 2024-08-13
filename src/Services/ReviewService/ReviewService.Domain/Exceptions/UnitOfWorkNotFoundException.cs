namespace ReviewService.Domain.Exceptions
{
    public class UnitOfWorkNotFoundException : Exception
    {
        public UnitOfWorkNotFoundException()
            : base("Unit of Work not found or not initialized.")
        {
        }
        public UnitOfWorkNotFoundException(string message) : base(message)
        {
        }
        public UnitOfWorkNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}