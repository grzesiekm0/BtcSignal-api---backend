using Btcsignal.Core.Inerfaces.Repositories;
using Btcsignal.Core.Inerfaces.Services;
using Btcsignal.Core.Models.Dao;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using Fluent.Infrastructure.FluentModel;
using System.Security.Claims;

namespace Btcsignal.Core.Services
{
    public class AlertService : IAlertService
    {
       private IAlertRepository _alertRepository;
        

       // public ClaimsPrincipal User { get; private set; }

        public AlertService(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;   
        }
        public async Task<IEnumerable<Alert>> GetAlertsAdmin()
        {
            var result = await _alertRepository.GetAlertsAdmin();
            return result;
        }
        public async Task<IEnumerable<Alert>> GetAlertsUser(string userId)
        {
             
            var result = await _alertRepository.GetAlertsUser(userId);
            return result;
        }
        public async Task<IEnumerable<Alert>> GetAlert(int alertId)
        {
            var result = await _alertRepository.GetAlert(alertId);
            return result;
        }
        
    }

}
