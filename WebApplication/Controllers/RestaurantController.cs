using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

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
        public IActionResult Index(string inputRoomNumber, string inputAdultNumber, string inputChildrenNumber)
        {
            return Ok();
        }
    }
}