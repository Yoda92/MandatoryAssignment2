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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(uint inputRoomNumber, uint inputAdultNumber, uint inputChildrenNumber)
        {
            Reservation thisReservation = _dbContext.Reservations.FirstOrDefault(r => r.RoomNumber == inputRoomNumber);
            if (thisReservation != null)
            {
                thisReservation.IsCheckedIn = true;
                _dbContext.Reservations.Update(thisReservation);
                await _dbContext.SaveChangesAsync();
                ViewBag.Message = "Room Number has been checked in.";
                return View();
            }
            else
            {
                ViewBag.Message = "Error! Room number does not exist!";
                return View();
            }
        }
    }
}