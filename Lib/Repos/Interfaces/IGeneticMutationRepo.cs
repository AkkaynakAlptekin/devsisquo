using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repos.Interfaces
{
    public interface IGeneticMutationRepo
    {
        Task Insert(GeneticMutation gm);
    }
}
