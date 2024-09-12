using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Endpoints
{
    public static class SurgeryEndpoint
    {
        //TODO:  add additional endpoints in here according to the requirements in the README.md 
        public static void ConfigurePatientEndpoint(this WebApplication app)
        {
            var surgeryGroup = app.MapGroup("surgery");

            surgeryGroup.MapGet("/patients", GetPatients);
            surgeryGroup.MapGet("/patient", GetOne);
            surgeryGroup.MapPost("/patient", CreateOne);
            surgeryGroup.MapGet("/doctors", GetDoctors);
            surgeryGroup.MapPost("/doctors", CreateDoctor);

            surgeryGroup.MapGet("/doctorsById", GetDoctor);

            surgeryGroup.MapGet("/appointmentsbydoctor/{id}", GetAppointmentsByDoctor);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPatients(IRepository repository)
        { 
            return TypedResults.Ok(await repository.GetPatients());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]

        public static async Task<IResult> GetOne(IRepository repository, int id)
        {
            return TypedResults.Ok(await repository.GetOne(id));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]

        public static async Task<IResult> CreateOne(IRepository repository, Patient patient)
        {
            return TypedResults.Ok(await repository.CreatePatient(patient));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        //Gets all doctors
        public static async Task<IResult> GetDoctors(IRepository repository)
        {
            var doctors = await repository.GetDoctors();
            List<DoctorDTO> results = new List<DoctorDTO>();
            foreach(var doctor in doctors)
            {
                DoctorDTO doctorDTO = new DoctorDTO();
                doctorDTO.FullName = doctor.FullName;
                doctorDTO.Appointments = doctor.Appointments.Select(x => new AppointmentDTO
                {
                    Booking = x.Booking,
                    Doctor = x.Doctor,
                    Patient = x.Patient
                }).ToList();
                results.Add(doctorDTO);
            }

            return TypedResults.Ok(results);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        //Get one doctor
        public static async Task<IResult> GetDoctor(IRepository repository, int id)
        {
            var doc = await repository.GetDoctorById(id);
            DoctorDTO docDTO = new DoctorDTO()
            {
                FullName = doc.FullName,
                Appointments = doc.Appointments.Select(x => new AppointmentDTO {
                    Booking = x.Booking,
                    Doctor = x.Doctor,
                    Patient = x.Patient 
                }).ToList()

            };

            return TypedResults.Ok(docDTO);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        //Adds one doctor
        public static async Task<IResult> CreateDoctor(IRepository repository, Doctor doc1)
        {
            return TypedResults.Ok(await repository.CreateDoctor(doc1));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsByDoctor(IRepository repository, int id)
        {
            return TypedResults.Ok(await repository.GetAppointmentsByDoctor(id));
        }

    }
}
