using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient> GetOne(int id);
        Task<Patient> CreatePatient(Patient patient);
        Task<IEnumerable<Doctor>> GetDoctors();
        Task <Doctor> GetDoctorById(int id);
        Task<IEnumerable<Doctor>> CreateDoctor(Doctor doc);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int id);

    }
}
