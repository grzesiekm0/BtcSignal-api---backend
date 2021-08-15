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
            /*var optionsBuilder = new DbContextOptions<BtcSignalDbContext>();
           *//* optionsBuilder.UseSqlServer(Configuration.GetConnectionStringSecureValue("DefaultConnection"));
            _context = new ApplicationDbContext(optionsBuilder.Options);*//*

            using (var context = new BtcSignalDbContext(optionsBuilder))
            {
                return await context.Alerts.AsNoTracking().FirstOrDefaultAsync(x => x.AlertId == alertId);
            }*/
            return await _context.Alerts.AsNoTracking().FirstOrDefaultAsync(x => x.AlertId == alertId);
        }
        public async Task<IEnumerable<Alert>> GetTest(int alertId)
        {
            /*var optionsBuilder = new DbContextOptions<BtcSignalDbContext>();
           *//* optionsBuilder.UseSqlServer(Configuration.GetConnectionStringSecureValue("DefaultConnection"));
            _context = new ApplicationDbContext(optionsBuilder.Options);*//*

            using (var context = new BtcSignalDbContext(optionsBuilder))
            {
                return await context.Alerts.AsNoTracking().FirstOrDefaultAsync(x => x.AlertId == alertId);
            }*/

            /*var result = (from b in _context.Alerts
                                where b.AlertId.Equals(alertId)
                                orderby b.AlertId descending
                                select b).DefaultIfEmpty().Where(x => x != null);*/
            //var entity;
            /*var optionsBuilder = new DbContextOptions<BtcSignalDbContext>();
            using (var context = new BtcSignalDbContext(optionsBuilder))
            {
                return await context.Alerts.ToListAsync();// FirstOrDefaultAsync(x => x.AlertId == alertId);
            }*/
            //return await _context.Alerts.ToListAsync();


         
            return await _context.Alerts
                .Where(file => file.AlertId == alertId)
                .ToListAsync();
            //var entity = await result.FirstOrDefaultAsync(x => x.AlertId == alertId);
            // var test = await _context.Alerts.FirstOrDefaultAsync(x => x.AlertId == alertId);
            //return "Test!!!!!!!!!!!!!!!! ===== "+result.Currency;
            // return entity;
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
