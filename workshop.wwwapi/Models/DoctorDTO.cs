namespace workshop.wwwapi.Models
{
    public class DoctorDTO
    {
        public string FullName { get; set; }
        public List<Appointment> Appointments { get; set; }

    }
}
