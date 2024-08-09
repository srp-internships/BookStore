namespace ReviewService.Application.Common.DTOs
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
