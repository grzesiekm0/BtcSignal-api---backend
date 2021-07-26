using Btcsignal.Core.Models.Dao;
using Btcsignal.Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Btcsignal.Core.Inerfaces.Services
{
    public interface IAlertService
    {
       Task<IEnumerable<Alert>> GetAlertsAdmin();
       Task<IEnumerable<Alert>> GetAlertsUser(string userId);
       Task<AlertCreateResponse> AddAlert(Alert item, string userId);
       Task<AlertCreateResponse> UpdateAlert(int alertId, Alert item, string userId);
       Task<Alert> GetAlert(int alertId);
    }
}
