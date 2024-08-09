using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Domain.Exceptions
{
    public class InvalidRatingException: Exception
    {
        public InvalidRatingException():base("Rating must be between 1 and 5.") { }
        public InvalidRatingException(string message) : base(message)
        {
        }

        public InvalidRatingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
