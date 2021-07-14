using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Btcsignal.Core.Inerfaces.Repositories;
using Btcsignal.Core.Models;
using Btcsignal.Core.Models.Dao;
using Microsoft.EntityFrameworkCore;

namespace Btcsignal.Infrastructures.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly BtcSignalDbContext _context;
        public AlertRepository(BtcSignalDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Alert>> GetAlert(int alertId)
        {
            var todoItem = await _context.Alerts.FindAsync(alertId);

            /*if (todoItem == null)
            {
                return NotFound();
            }*/

           return (IEnumerable<Alert>)todoItem;
        }

        public async Task<IEnumerable<Alert>> GetAllAlerts()
        {
            return await _context.Alerts.ToListAsync();
        }
    }
}
