namespace ReviewService.Domain.Exceptions
{
    public class EmptyCommentException:Exception
    {
        public EmptyCommentException():base("Comment cannot be enpty ") { }
        public EmptyCommentException(string message) : base(message) { }
        public EmptyCommentException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
