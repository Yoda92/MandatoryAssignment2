using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public RestaurantController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Reservations.Where(r => r.Date.Date == DateTime.Now.Date 
                                                                 && r.Date.Month == DateTime.Now.Month
                                                                 && r.Date.Year == DateTime.Now.Year).ToListAsync());
        }
        public IActionResult CheckIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCheckIn(int RoomNumber, int NumberOfAdultsCheckedIn, int NumberOfChildrenCheckedIn)
        {
            Reservation thisReservation = _dbContext.Reservations.FirstOrDefault(r => r.RoomNumber == RoomNumber);
            if (thisReservation != null)
            {
                thisReservation.IsCheckedIn = true;
                thisReservation.NumberOfAdultsCheckedIn = NumberOfAdultsCheckedIn;
                thisReservation.NumberOfChildrenCheckedIn = NumberOfChildrenCheckedIn;
                _dbContext.Reservations.Update(thisReservation);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), "Restaurant");
        }
    }
}