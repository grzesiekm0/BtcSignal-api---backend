using Btcsignal.Core.Models.Dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Btcsignal.Core.Inerfaces.Repositories
{
    public interface IAlertRepository
    {
        Task<IEnumerable<Alert>> GetAllAlerts();
        Task<IEnumerable<Alert>> GetAlert(int alertId);
    }
}
