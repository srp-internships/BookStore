using ReviewService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Application.IRepositories
{
    public interface IReviewRepository
    {
        Task<Review> GetByIdAsync(Guid id); // Получить отзыв по идентификатору
        Task<IEnumerable<Review>> GetByBookIdAsync(Guid bookId); // Получить все отзывы для книги
        Task<IEnumerable<Review>> GetByUserIdAsync(Guid userId); // Получить все отзывы пользователя
        Task AddAsync(Review review); // Добавить новый отзыв
        Task UpdateAsync(Review review); // Обновить существующий отзыв
        Task DeleteAsync(Guid id);
    }
}
