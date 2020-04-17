using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize(Policy = "ReceptionistsOnly")]
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
        public async Task<IActionResult> Today()
        {
            return View("Index",await _dbContext.Reservations.Where(r=>r.Date.Date==DateTime.Today).ToListAsync());
        }
        
        
        [HttpGet]
        public async Task<IActionResult> CheckedInToday()
        {
            
            ViewBag.listOfReservations = await _dbContext.Reservations.Where(r=>r.NumberOfAdultsCheckedIn!=0 || r.NumberOfChildrenCheckedIn!=0).ToListAsync();
            
            
            
            ViewBag.totalCount = _dbContext.Reservations.Where(r => r.Date == DateTime.Today).ToList().Aggregate(new int(),
                (number, reservation) =>
                {
                    number += reservation.NumberOfAdultsCheckedIn + reservation.NumberOfChildrenCheckedIn;
                    return number;
                }

            );
            
            return View(ViewBag);
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
        //[Bind("Date,RoomNumber,NumberOfAdults,NumberOfChildren)]"
        [HttpPost]
        public async Task<IActionResult> CreateReservation(Reservation reservation)
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