using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;


namespace WebApplication.Controllers
{
    public class ReceptionController
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
        
    }
}