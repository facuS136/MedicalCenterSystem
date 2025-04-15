using Microsoft.EntityFrameworkCore;
using MedSys.Models;

namespace MedSys.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración, inyectadas por el contenedor de dependencias.
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Cada DbSet representa una "tabla" en la base de datos.
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<HistorialMedico> HistorialesMedicos { get; set; }

        // Opcional: Configuración adicional de los modelos en la base de datos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aquí puedes configurar relaciones, restricciones o valores por defecto.
            // Por ejemplo, si quisieras especificar que el campo Username en Usuario es único,
            // podrías hacerlo de esta manera (suponiendo que uses EF Core con una base de datos relacional):
            // modelBuilder.Entity<Usuario>()
            //     .HasIndex(u => u.Username)
            //     .IsUnique();
        }
    }
}
