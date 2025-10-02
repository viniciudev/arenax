using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class TokenAppRequest
    {
        public int IdClient { get; set; }
        public required string Token { get; set; }
    }
}
