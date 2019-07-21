using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using btcsignalwebservice.Model;

namespace btcsignalwebservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly AlertContext _context;

        public AlertController(AlertContext context)
        {
            _context = context;

            if (_context.Alert.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Alert.Add(new Alert { Name = "Item1" });
                _context.SaveChanges();
            }
        }
    }
}
