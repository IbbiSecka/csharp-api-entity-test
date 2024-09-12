using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DatabaseContext _databaseContext;
        public Repository(DatabaseContext db)
        {
            _databaseContext = db;
        }
        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _databaseContext.Patients.Include(x => x.Appointments).ToListAsync();
        }
        public async Task <List<Doctor>> GetDoctors()
        {
            return await _databaseContext.Doctors.Include(x => x.Appointments).ToListAsync();
        }
     

        public async Task<Patient> GetOne(int id)
        {
            var onePatient =  await _databaseContext.Patients.FirstOrDefaultAsync(x => x.Id==id);
            return onePatient;
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
             _databaseContext.Patients.Add(patient);
             _databaseContext.SaveChangesAsync();
            return patient;
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
           return await  _databaseContext.Doctors.FirstOrDefaultAsync(x =>x.Id==id);

        }

        public async Task<Doctor> CreateDoctor(Doctor doc)
        {
            
            await _databaseContext.AddAsync(doc);
            return doc;
        }



        public async Task<List<Appointment>> GetAppointmentsByDoctor(int id)
        {
            return await _databaseContext.Appointments.Where(x => x.DoctorId == id).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            return await _databaseContext.Appointments
                .Include(x => x.DoctorName)
                .Include(x => x.PatientName)

                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            return await _databaseContext.Appointments
                .Include(x => x.DoctorName)
                .Include(x => x.PatientName)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Appointment>> GetAppointmentByPatientId(int id)
        {
            return await _databaseContext.Appointments.Where(x => x.PatientId == id).ToListAsync();
        }

        public async Task<Appointment> CreateAppointment(Appointment checkup)
        {
            await _databaseContext.Appointments.AddAsync(checkup);
            return checkup;
        }
    }
}
