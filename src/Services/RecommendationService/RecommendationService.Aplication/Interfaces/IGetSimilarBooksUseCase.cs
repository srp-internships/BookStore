using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationService.Aplication.Interfaces
{
    public interface IGetSimilarBooksUseCase
    {
        List<string> Execute(string bookId);
    }
}
