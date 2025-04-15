using MedSys.Models;

namespace MedSys.DTOs.Requests
{
    public class PacienteRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? DNI { get; set; }
        public string? Telefono { get; set; }
        public string? ObraSocial { get; set; }
    }

    public class NewPacienteRequest : PacienteRequest
    {
        public Paciente ToPaciente()
        {
            return new Paciente
            {
                Nombre = this.Nombre,
                Apellido = this.Apellido,
                DNI = this.DNI,
                Telefono = this.Telefono,
                ObraSocial = this.ObraSocial
            };
        }
    }

    public class UpdatePacienteRequest : PacienteRequest
    {
        public int Id { get; set; }

        internal void DoUpdate(Paciente? paciente)
        {
            paciente.Nombre = this.Nombre;
            paciente.Apellido = this.Apellido;
            paciente.DNI = this.DNI;
            paciente.Telefono = this.Telefono;
            paciente.ObraSocial = this.ObraSocial;
        }
    }
}
