using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ReceptionController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ReceptionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Reservations.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> GetReservationsByDay(DateTime date)
        {
            return View(await _dbContext.Reservations.Where(r=> r.Date == date).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SaveReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Reservations.Add(reservation);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(SaveConfirmation), "Reception",reservation.);
            }
            
            return View( );
        }
    }
}