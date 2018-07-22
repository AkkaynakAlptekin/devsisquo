using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repos.Interfaces
{
    public interface IGeoTrackingRepo
    {
        Task Insert(GeoTracking gt);
        Task InsertWithMetaData(GeoTracking gt, RaptorMetaData rmd);
    }
}
