namespace WebApplication.DTOs
{
    public class AccumulatedReservations
    {
        public int ExpectedGuest { get; set; }
        public int CheckedInChildren { get; set; }
        public int CheckedInAdults { get; set; }
        public int NotCheckedInGuests { get; set; }
        public int NotCheckedInChildren { get; set; }
        public int NotCheckedInAdults { get; set; }
    }
}