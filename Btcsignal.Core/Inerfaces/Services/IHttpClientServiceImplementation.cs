using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Btcsignal.Core.Inerfaces.Services
{
    public interface IHttpClientServiceImplementation
    {
        public Task Execute();
        public Task GetCompaniesWithHttpClientFactory();
    }

}
