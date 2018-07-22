using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPApi.Models
{
    public class GeoTrackingMawDtp
    {
        public int Id { get; set; }
        public Decimal X { get; set; }
        public Decimal Y { get; set; }
        public int IdRaptor { get; set; }
        public int IdWinningGeneticMutation { get; set; }
        public DateTime Tempo { get; set; }
        public int PersoneMangiate { get; set; }
        public Boolean FauciAperte { get; set; }
    }
}
