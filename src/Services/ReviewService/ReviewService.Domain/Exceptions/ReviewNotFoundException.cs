namespace ReviewService.Domain.Exceptions
{
    public class ReviewNotFoundException:Exception
    {
        public ReviewNotFoundException(Guid reviewId):
            base($"Review with ID {reviewId} was not found."){ }
        public ReviewNotFoundException(string message) : 
            base(message){}
        public ReviewNotFoundException(string message, Exception innerException) :
            base(message, innerException){ }
    }
}
