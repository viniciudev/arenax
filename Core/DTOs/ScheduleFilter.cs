using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public  class ScheduleFilter
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public int IdSportsCourt { get; set; }
        public SchedulingStatus? Status { get; set; }
        public int? IdClient { get; set; }
        public int? IdCategory { get; set; }
        public int? IdSportsCenter { get; set; }
        //numero de vagas
        public int ?Number { get; set; }
        public decimal Value { get; set; }
        public bool OpenPublic { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
