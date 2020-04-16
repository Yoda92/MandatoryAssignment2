using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int NumberOfAdults { get; set; }
        [Required]
        public int NumberOfChildren { get; set; }
        public int NumberOfAdultsCheckedIn { get; set; }
        public int NumberOfChildrenCheckedIn { get; set; }
        public bool IsCheckedIn { get; set; }
    }
}