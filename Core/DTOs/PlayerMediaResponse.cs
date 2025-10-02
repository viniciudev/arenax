using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class PlayerMediaResponse
    {
        public decimal Average { get; set; }
        public List<ClientEvaluation> ClientEvaluations { get; set; } = new List<ClientEvaluation>();
    }
}
