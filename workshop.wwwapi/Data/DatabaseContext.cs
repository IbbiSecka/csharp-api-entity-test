using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Data
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString;
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Example.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Appointment Key etc.. Add Here
            modelBuilder.Entity<Appointment>().HasKey(a => new { a.PatientId, a.DoctorId });

            //Relationshios
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(x => x.Doctor)
                .WithMany(x => x.Appointments)
               
                .HasForeignKey(k => k.DoctorId);

            //TODO: Seed Data Here
            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, FullName = "Ibbi Secka" },
                new Patient { Id = 2, FullName = "Silly Sillah" }
            );
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, FullName = "Doofenschmirtz" },
                new Doctor { Id = 2, FullName = "Dr. Robotnik" },
                new Doctor { Id = 3, FullName = "Dr Banner" }
                );
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    DoctorId = 1,
                    PatientId = 1,
                    Booking = new DateTime(2023, 10, 30, 09, 0, 0)
                },
                new Appointment
                {
                    PatientId = 2,
                    DoctorId = 1,
                    Booking = new DateTime(2024, 09, 16, 10, 0, 0)
                },
                new Appointment
                {
                    PatientId = 2,
                    DoctorId = 3,
                    Booking = new DateTime(2024, 09, 16, 10, 0, 0)
                }
                );


            
            base.OnModelCreating(modelBuilder);
        }

     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "Database");
            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message)); //see the sql EF using in the console
            
        }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
