using System;
using System.Collections.Generic;
using System.Text;

namespace Lib
{
    public class GeoTracking
    {
        public int Id { get; set; }
        public Decimal X { get; set; }
        public Decimal Y { get; set; }
        public int IdRaptor { get; set; }
        public int IdWinningGeneticMutation { get; set; }
        public DateTime Tempo { get; set; }

    }
}
