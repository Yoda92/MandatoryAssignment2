using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsReceptionist { get; set; }
        public bool IsWaiter { get; set; }
    }
}