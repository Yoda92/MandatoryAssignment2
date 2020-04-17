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
        public IActionResult CheckIn(int Id)
        {
            return View(_dbContext.Reservations.FirstOrDefault(r => r.Id == Id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCheckIn(int Id, int numberOfAdultsCheckedIn, int numberOfChildrenCheckedIn)
        {
            Reservation thisReservation = _dbContext.Reservations.FirstOrDefault(r => r.Id == Id);
            if (thisReservation != null)
            {
                if ((thisReservation.NumberOfAdults - thisReservation.NumberOfAdultsCheckedIn - numberOfAdultsCheckedIn ) >= 0) thisReservation.NumberOfAdultsCheckedIn += numberOfAdultsCheckedIn;
                if ((thisReservation.NumberOfChildren - thisReservation.NumberOfChildrenCheckedIn - numberOfChildrenCheckedIn) >= 0) thisReservation.NumberOfChildrenCheckedIn += numberOfChildrenCheckedIn;
                _dbContext.Reservations.Update(thisReservation);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), "Restaurant");
        }
    }
}