using Btcsignal.Core.Models.Dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Btcsignal.Core.Inerfaces.Services
{
    public interface IAlertService
    {
       Task<IEnumerable<Alert>> GetAllAlerts();
       Task<IEnumerable<Alert>> GetAlert(int alertId);
    }
}
