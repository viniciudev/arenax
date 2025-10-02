using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Notifications : BaseModel
    {
        public required string Title { get; set; }
        public required string Body { get; set; }
        public bool Read { get; set; } = false;
        public int? IdClient { get; set; }
        public Client Client { get; set; }
        
    }
}
