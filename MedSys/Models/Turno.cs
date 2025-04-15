using MedSys.DTOs.Responses;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedSys.Models
{
    public class Turno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Id del usuario es obligatorio.")]
        public required int IdUser { get; set; }

        [Required(ErrorMessage = "El Id del doctor es obligatorio.")]
        public required int IdDoctor { get; set; }

        [Required(ErrorMessage = "El Id del paciente es obligatorio.")]
        public required int IdPaciente { get; set; }

        [Required(ErrorMessage = "El día del turno es obligatorio.")]
        public required DateOnly Dia { get; set; }

        [Required(ErrorMessage = "La hora del turno es obligatoria.")]
        public required TimeSpan Hora { get; set; }

        internal TurnoResponse ToTurnoResponse()
        {
            return new TurnoResponse(this);
        }
    }
}
