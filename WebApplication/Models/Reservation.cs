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
        public uint RoomNumber { get; set; }
        [Required]
        public uint NumberOfAdults { get; set; }
        [Required]
        public uint NumberOfChildren { get; set; }
    }
}