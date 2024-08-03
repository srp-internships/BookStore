using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using RecommendationService.Application.Interfaces;
using RecommendationService.Contracts;

namespace RecommendationService.Application
{
    public class ServiceRecommend : Recommendation.RecommendationBase
    {
        private readonly IApplicationDbContext _dbContext;
        public ServiceRecommend(IApplicationDbContext dbContext)
        {
                _dbContext = dbContext;
        }
        public override async Task<BookListResponse> GetPopularBooks(BookRequest request, ServerCallContext context)
        {
            var bookRequest= await _dbContext.Books.FindAsync(request.BookId);
            
            if (bookRequest == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));
            }
            var bookCategory = _dbContext.Books.Where(c => c.CategoriesIds == bookRequest.CategoriesIds);

            var popularBooks = new BookListResponse();

            foreach (var item in bookCategory)
            {
                popularBooks.BookIds.Add(item.Id.ToString());

            }
           return popularBooks;
        }
        public override async Task<BookListResponse> GetSimilarBooks(BookRequest request, ServerCallContext context)
        {
            var bookRequest = await _dbContext.Books.FindAsync(request.BookId);

            if (bookRequest == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));
            }
            var bookAuthors = _dbContext.Books.Where(c => c.AuthorId == bookRequest.AuthorId);

            var similarBooks = new BookListResponse();

            foreach (var item in bookAuthors)
            {
                similarBooks.BookIds.Add(item.Id.ToString());

            }
            return similarBooks;
        }
    }
}
