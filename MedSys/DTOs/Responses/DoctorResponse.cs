using MedSys.Models;

namespace MedSys.DTOs.Responses
{
    public class DoctorResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Especialidad { get; set; }
        public string Username { get; set; }
        public DoctorResponse(Doctor doctor)
        {
            Id = doctor.Id;
            Nombre = doctor.Nombre;
            Apellido = doctor.Apellido;
            DNI = doctor.DNI;
            Especialidad = doctor.Especialidad;
            Username = doctor.Username;
        }
    }
}
