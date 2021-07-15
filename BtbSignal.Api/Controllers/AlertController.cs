using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Btcsignal.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Btcsignal.Core.Models.Dao;
using Btcsignal.Core.Inerfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace btcsignalwebservice.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]

    public class AlertController : ControllerBase
    {
        private readonly BtcSignalDbContext _context;
        private IAlertService _alertService;
        private UserManager<IdentityUser> _userManger;

        public AlertController(BtcSignalDbContext context, IAlertService alertService, UserManager<IdentityUser> userManger)
        {
            _context = context;
            _alertService = alertService;
            _userManger = userManger;
        }

        // GET: api/alert/AlertsForAdmin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("AlertsForAdmin")]
        public async Task<IActionResult> GetAlertsAdmin() => Ok(await _alertService.GetAlertsAdmin());

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("AlertsForUser")]
        public async Task<IActionResult> GetAlertsUser() 
        {
            IdentityUser appUser = await _userManger.GetUserAsync(User);
            string userId = appUser?.Id; 
            var result = await _alertService.GetAlertsUser(userId);

            return Ok(result);
        }

        // GET: api/alert/AlertsForUser
        [HttpGet("{id}")]
        public async Task<ActionResult<Alert>> GetAlert(int id) => Ok(await _alertService.GetAlert(id));

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
