using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.DTOs;
using WebApplication.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    public class KitchenController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public KitchenController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            AccumulatedReservations accReservation = _dbContext.Reservations.ToList().Aggregate(new AccumulatedReservations(), (acc, r) =>
                {
                    acc.ExpectedGuest += r.NumberOfAdults + r.NumberOfChildren;
                    acc.CheckedInAdults += r.NumberOfAdultsCheckedIn;
                    acc.CheckedInChildren += r.NumberOfChildrenCheckedIn;
                    acc.NotCheckedInAdults += r.NumberOfAdults - r.NumberOfAdultsCheckedIn;
                    acc.NotCheckedInChildren += r.NumberOfChildren - r.NumberOfChildrenCheckedIn;
                    acc.NotCheckedInGuests += r.NumberOfChildren + r.NumberOfAdults - r.NumberOfAdultsCheckedIn -
                                              r.NumberOfChildrenCheckedIn;
                    return acc;
                });

                        
            return View(accReservation);
        }
        
    }
}
