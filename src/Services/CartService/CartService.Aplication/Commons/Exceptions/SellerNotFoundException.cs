using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Commons.Exceptions
{
    public class SellerNotFoundException : Exception
    {
        public SellerNotFoundException(string message) : base(message)
        {
        }
    }
}
