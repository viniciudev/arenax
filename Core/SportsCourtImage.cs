using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SportsCourtImage:BaseModel
    {
        public int SportsCourtId { get; set; }
        public SportsCourt SportsCourt { get; set; }
        public required string UniqueFileName { get; set; }

        [NotMapped]
        public string? ImageUrl{ get; set; }
    }
}
