using MedSys.Models;

namespace MedSys.DTOs.Responses
{
    public class PacienteResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string ObraSocial { get; set; }
        public PacienteResponse(Paciente paciente)
        {
            Id = paciente.Id;
            Nombre = paciente.Nombre;
            Apellido = paciente.Apellido;
            DNI = paciente.DNI;
            Telefono = paciente.Telefono;
            ObraSocial = paciente.ObraSocial;
        }
    }
}
