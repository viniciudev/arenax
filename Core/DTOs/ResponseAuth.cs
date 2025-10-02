using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ResponseAuth
    {
        public  required string Message { get; set; }
        public TokenJWT? TokenJWT { get; set; }
    }
}
