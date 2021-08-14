using Btcsignal.Core.Inerfaces.Services;
using Btcsignal.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorkerDemoService.Jobs
{
    class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
       // private readonly IServiceProvider _serviceProvider;
        private readonly IAlertService _AlertService;

        public NotificationJob(ILogger<NotificationJob> logger, IAlertService alertService)
        {
            this._logger = logger;
            //   this._serviceProvider = serviceProvider;
            _AlertService = alertService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            // fetch customers, send email, update DB
            _logger.LogInformation($"Notify User at {DateTime.Now} and test get alert: { _AlertService.GetAlertsUser("2")}");

            return Task.CompletedTask;
        }


        /*public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var jobType = context.JobDetail.JobType;
                var job = scope.ServiceProvider.GetRequiredService(jobType) as IJob;

                var alertSer = scope.ServiceProvider.GetRequiredService<IAlertService>();
                //var dbContext = _serviceProvider.GetRequiredService<BtcSignalDbContext>();

                //await job.Execute(context);
                //var messageBus = _serviceProvider.GetRequiredService<IBus>();
                //alertSer.GetAlert(6);
                _logger.LogInformation($"Notify User at {DateTime.Now} and test get alert: { alertSer.GetAlertsUser("2")}");


               // await job.Execute(context);

                // job completed, save dbContext changes
                //await dbContext.SaveChangesAsync();

                // db transaction succeeded, send messages
                //await messageBus.DispatchAsync();*/
    }

            //_alertService.GetAlert(6);
            //_logger.LogInformation($"Notify User at {DateTime.Now} and test get alert: {_alertService.GetAlert(6)}");
          //  return Task.CompletedTask;
        //}

}
