using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Btcsignal.Core.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Btcsignal.Core.Models.Dao;
using Btcsignal.Core.Inerfaces.Services;

namespace btcsignalwebservice.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]

    public class AlertController : ControllerBase
    {
        private readonly BtcSignalDbContext _context;
        private IAlertService _alertService;

        public AlertController(BtcSignalDbContext context, IAlertService alertService)
        {
            _context = context;
            _alertService = alertService;


        }

        // GET: api/alert test
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("AlertsForAdmin")]
        public async Task<ActionResult<IEnumerable<Alert>>> GetAlertsAdmin() => Ok(await _alertService.GetAllAlerts());
        /*{
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            return await _context.Alerts.ToListAsync();
        }*/



        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("AlertsForUser")]
        public async Task<ActionResult<IEnumerable<Alert>>> GetAlertsUser()
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            return await _context.Alerts.ToListAsync();
        }

        // GET: api/alert/AlertsForUser
        [HttpGet("{id}")]
        public async Task<ActionResult<Alert>> GetAlert(int id) => Ok(await _alertService.GetAlert(id));
        /*{
            var todoItem = await _context.Alerts.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }*/


        // POST: api/alert
        [HttpPost]
        public async Task<ActionResult<Alert>> PostAlert(Alert item)
        {
            _context.Alerts.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlert), new { id = item.AlertId }, item);
        }

        // PUT: api/alert/id
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> PutAlert(int id, Alert item)
        {
            if (id != item.AlertId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();  
        }

        // DELETE: api/alert/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id)
        {
            var todoItem = await _context.Alerts.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Alerts.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
