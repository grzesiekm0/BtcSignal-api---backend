using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Btcsignal.Core.Inerfaces.Repositories;
using Btcsignal.Core.Models;
using Btcsignal.Core.Models.Dao;
using Microsoft.EntityFrameworkCore;
using Btcsignal.Core.Models.Responses;

namespace Btcsignal.Infrastructures.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly BtcSignalDbContext _context;

        public AlertRepository(BtcSignalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alert>> GetAlertsAdmin()
        {
            return await _context.Alerts.ToListAsync();
        }
        
        public async Task<IEnumerable<Alert>> GetAlertsUser(string userId)
        {
                var result = await (from b in _context.Alerts
                                   where b.UserId.Equals(userId)
                                   orderby b.AlertId descending
                                   select b).ToListAsync();
           
            return result;
        }

        public async Task<AlertCreateResponse> AddAlert(Alert item)
        {
             _context.Alerts.Add(item);
            await _context.SaveChangesAsync();
            return new AlertCreateResponse { 
                Id = item.AlertId,
                Message = "Successful addition!"
            };
        }

        public async Task<AlertCreateResponse> UpdateAlert(Alert item)
        {
            var response = new AlertCreateResponse();

            _context.Entry(item).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                response.Message = "Failed edition to db!";
            }

            response.Id = item.AlertId;
            response.Message = "Successful edition!";

            return response;
        }

        public async Task<Alert> GetAlert(int alertId)
        {

            return await _context.Alerts.AsNoTracking().FirstOrDefaultAsync(x => x.AlertId == alertId);
        }

        public async Task<bool> OnOffAlert(int alertId, bool onOff)
        {
            Alert alert = new Alert() { AlertId = alertId, Active = onOff };

            _context.Alerts.Attach(alert);
            _context.Entry(alert).Property(x => x.Active).IsModified = true;
            var result = await _context.SaveChangesAsync();
           
            //_context.Entry(item).State = EntityState.Modified;
            //var result = await _context.SaveChangesAsync();
            return alert.Active;
        }
    }
}
