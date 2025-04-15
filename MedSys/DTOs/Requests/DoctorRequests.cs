using MedSys.Models;

namespace MedSys.DTOs.Requests
{
    public class DoctorRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? DNI { get; set; }
        public string? Especialidad { get; set; }
        public string? Username { get; set; }
    }

    public class NewDoctorRequest : DoctorRequest
    {
        public string? Clave { get; set; }

        public Doctor ToDoctor()
        {
            return new Doctor
            {
                // Puede ser NULL
                Nombre = this.Nombre,
                Apellido = this.Apellido,
                DNI = this.DNI,
                Especialidad = this.Especialidad,
                Username = this.Username,
                Clave = this.Clave
            };
        }
    }

    public class UpdateDoctorRequest : DoctorRequest
    {
        public int Id { get; set; }

        internal void DoUpdate(Doctor doctor)
        {
            doctor.Nombre = this.Nombre;
            doctor.Apellido = this.Apellido;
            doctor.DNI = this.DNI;
            doctor.Especialidad = this.Especialidad;
            doctor.Username = this.Username;
        }
    }
}
