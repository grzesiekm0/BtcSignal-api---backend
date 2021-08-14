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

        public Task Execute(IJobExecutionContext context)
        {
           // _logger.LogInformation("Hello world!");
            _logger.LogInformation($"Notify User at {DateTime.Now} and test get alert: {_AlertRepository.GetTest(12)}");

            return Task.CompletedTask;
        }
    }
}
