using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPApi.Services.Interfaces
{
    public interface IEventHubService
    {
        Task SendMsg(string msg);
    }
}
