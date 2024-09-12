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
                    DoctorName = x.DoctorName,
                    PatientName = x.PatientName
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
                    DoctorName = x.DoctorName,
                    PatientName = x.PatientName 
                }).ToList()

            };

            return TypedResults.Ok(docDTO);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        //Adds one doctor
        public static async Task<IResult> CreateDoctor(IRepository repository, Doctor doc1)
        {
            if (doc1 == null) {
                return TypedResults.NotFound();
            }
            var createdDoc = await repository.CreateDoctor(doc1);

            
            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsByDoctor(IRepository repository, int docId)
        {
            var appointment = await repository.GetAppointmentsByDoctor(docId);

            var appointmentDTO = appointment.Select(appointment => new AppointmentDTO()
            {
                
                Booking = appointment.Booking,
                PatientName =appointment.PatientName
            }).ToList();

            return TypedResults.Ok();
        }
        //public static async Task<IResult> GetAllAppointments(IRepository repository)
        //{

        //}
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllAppointments(IRepository repository)
        {
            var appointments = await repository.GetAppointments();
            List<AppointmentDTO> results = new List<AppointmentDTO>();
            foreach (var app in appointments) {

                AppointmentDTO appointmentDTO = new AppointmentDTO();
                appointmentDTO.Booking = app.Booking;
                appointmentDTO.PatientName = app.PatientName;
                appointmentDTO.DoctorName = app.DoctorName;

                results.Add(appointmentDTO);
                
            }
            return TypedResults.Ok(results);
        }
        public static async Task<IResult> GetAppById(IRepository repository, int id)
        {
            var appointment = repository.GetAppointmentById(id);
            if (appointment == null)
            {
                return TypedResults.NotFound("Appointment not found.");
            }
            var appointmentDTO = new AppointmentDTO();
            appointmentDTO.PatientName = appointment.Result.PatientName;
            appointmentDTO.DoctorName= appointment.Result.DoctorName;

            return TypedResults.Ok(appointmentDTO);
        }
        public static async Task<IResult> GetAppointmentByPatientId(IRepository repository, int id)
        {
            try
            {
                var appointment = repository.GetAppointmentByPatientId(id);
                List <AppointmentDTO> results = new List<AppointmentDTO>();
                foreach (var app in results) {
                var appointmentDTO = new AppointmentDTO();
                    appointmentDTO.PatientName = app.PatientName;
                    appointmentDTO.DoctorName = app.DoctorName;
                }
                return TypedResults.Ok(results);
            }
            catch (Exception)
            {

                return TypedResults.NotFound("Patient not found!");
               
            }
            
        }
        public static async Task<IResult> CreatAppointment(IRepository repo, Appointment appointment)
        {
            try
            {

            }
            catch (Exception)
            {

                TypedResults.("Not an appointment")M
            }
        }

    }
}
