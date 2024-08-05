using ReviewService.Domain.Entities;
using ReviewService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Application.Services
{
    public interface IUnitOfWork:IDisposable
    {
        IReviewRepository Reviews { get; }
        Task<int> CompleteAsync();
    }
}
