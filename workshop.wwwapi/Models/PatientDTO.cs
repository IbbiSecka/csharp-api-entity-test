namespace workshop.wwwapi.Models
{
    public class PatientDTO
    {
        public string Fullname {  get; set; }
        public List<AppointmentDTO> Appointments { get; set; }

    }
}
