namespace workshop.wwwapi.Models
{
    public class DoctorDTO
    {
        public string FullName { get; set; }
        public List<AppointmentDTO> Appointments { get; set; }

    }
}
