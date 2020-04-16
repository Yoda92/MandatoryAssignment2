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

        
        [HttpGet]
        public async Task<IActionResult> CreateReservation()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateReservation([Bind("Date,RoomNumber,NumberOfAdults,NumberOfChildren")]Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Reservations.Add(reservation);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Reception");
            }
            return View(reservation);
        }
    }
}