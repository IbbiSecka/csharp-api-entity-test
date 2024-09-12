namespace workshop.wwwapi.Models
{
    public class AppointmentDTO
    {
        public Patient Patient { get; set; }
        public DateTime Booking { get; set; }
        public Doctor Doctor { get; set; }
    }
}
