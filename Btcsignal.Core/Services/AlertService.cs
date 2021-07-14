using Btcsignal.Core.Inerfaces.Repositories;
using Btcsignal.Core.Inerfaces.Services;
using Btcsignal.Core.Models.Dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Btcsignal.Core.Services
{
    public class AlertService : IAlertService
    {
        IAlertRepository _alertRepository;
        public AlertService(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }
        public async Task<IEnumerable<Alert>> GetAlertsAdmin()
        {
            var result = await _alertRepository.GetAlertsAdmin();
            return result;
        }
        public async Task<IEnumerable<Alert>> GetAlert(int alertId)
        {
            var result = await _alertRepository.GetAlert(alertId);
            return result;
        }
    }

}
