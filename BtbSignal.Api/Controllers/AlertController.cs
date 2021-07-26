using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Btcsignal.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Btcsignal.Core.Models.Dao;
using Btcsignal.Core.Inerfaces.Services;
using Microsoft.AspNetCore.Identity;
using Btcsignal.Core.Models.Responses;

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

        // GET: api/alert/AlertsForUser
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

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Alert>> GetAlert(int id) => Ok(await _alertService.GetAlert(id));

        // POST: api/alert/AddAlert
        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        [Route("AddAlert")] 
        public async Task<AlertCreateResponse> AddAlert(Alert item)
        {
            IdentityUser appUser = await _userManger.GetUserAsync(User);
            string userId = appUser?.Id;
            var result = await _alertService.AddAlert(item, userId);
    
            return result;
        }

        // PUT: api/alert/id
        [HttpPut("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<AlertCreateResponse> UpdateAlert(int id, Alert item)
        {
            IdentityUser appUser = await _userManger.GetUserAsync(User);
            string userId = appUser?.Id;
            var result = await _alertService.UpdateAlert(id, item, userId);
    
            return result;
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
