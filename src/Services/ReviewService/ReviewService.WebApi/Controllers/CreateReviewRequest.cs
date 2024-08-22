namespace ReviewService.WebApi.Controllers
{
    public class CreateReviewRequest
    {
        public Guid BookId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
    }
}
