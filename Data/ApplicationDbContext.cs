
using MedicarePlus.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicarePlus.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PatientNote> PatientNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Role & User - One-to-Many **
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // User & Doctor - One-to-One
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne(u => u.doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User & Receptionist - One-to-One
            modelBuilder.Entity<Receptionist>()
                .HasOne(r => r.User)
                .WithOne(u => u.receptionist)
                .HasForeignKey<Receptionist>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Doctor & Specialization - One-to-Many
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient & Appointment - One-to-Many
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Doctor & Appointment - One-to-Many
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment & PatientNote - One-to-Many
            modelBuilder.Entity<PatientNote>()
                .HasOne(pn => pn.Appointment)
                .WithMany(a => a.PatientNotes)
                .HasForeignKey(pn => pn.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
               
        }
        
    }
}
