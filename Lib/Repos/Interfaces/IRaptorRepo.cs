using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repos.Interfaces
{
    public interface IRaptorRepo
    {
        Task<Raptor> GetAll();
        Task Insert(Raptor raptor);
        Task<List<TrackRaptExternal>> TrackAllRaptors();
    }
}
