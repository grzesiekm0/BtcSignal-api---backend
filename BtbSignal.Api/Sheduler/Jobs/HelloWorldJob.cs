using Btcsignal.Core.Inerfaces.Repositories;
using Btcsignal.Core.Inerfaces.Services;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BtcSignal.Api.Sheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;
        private readonly IAlertRepository _AlertRepository;
        public HelloWorldJob(ILogger<HelloWorldJob> logger, IAlertRepository alertService)
        {
            _logger = logger;
            _AlertRepository = alertService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string cos = "";
           // _logger.LogInformation("Hello world!");
           foreach(var i in await _AlertRepository.GetTest(12))
                {
                cos = i.Currency;
            }
            _logger.LogInformation($"Notify User at {DateTime.Now} and test get alert: { cos }");

            //return Task.CompletedTask;
        }
    }
}
