using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationService.Aplication.Interfaces
{
    public interface IGetPopularBooksUseCase
    {
        List<string> Execute(string bookId);
    }
}
