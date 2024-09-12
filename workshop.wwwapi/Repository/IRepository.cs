using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient> GetOne(int id);
        Task<Patient> CreatePatient(Patient patient);
        Task <List<Doctor>> GetDoctors();
        Task <Doctor> GetDoctorById(int id);
        Task <Doctor> CreateDoctor(Doctor doc);

        Task<List<Appointment>> GetAppointmentsByDoctor(int id);
        Task<List<Appointment>> GetAppointments();
        Task<Appointment> GetAppointmentById(int id);
        Task<List<Appointment>> GetAppointmentByPatientId(int id);
        Task<Appointment> CreateAppointment(Appointment check);

    }
}
