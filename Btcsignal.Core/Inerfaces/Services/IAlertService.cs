using Btcsignal.Core.Models.Dao;
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
       Task<IEnumerable<Alert>> GetAlert(int alertId);
    }
}
