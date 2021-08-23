using Microsoft.Extensions.DependencyInjection;
using System;

namespace Btcsignal.Infrastructures.Untils
{
    public class HttpClientUtilsFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public HttpClientUtilsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public HttpClientUtils Create()
        {
            return _serviceProvider.GetRequiredService<HttpClientUtils>();
        }        
    }
}
