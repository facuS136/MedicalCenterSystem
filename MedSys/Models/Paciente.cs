using MedSys.DTOs.Responses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedSys.Models
{
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50)]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50)]
        public required string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [StringLength(20)]
        public required string DNI { get; set; }

        /*
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        [StringLength(100)]
        public required string Correo { get; set; }
        */

        [StringLength(20)]
        public required string Telefono { get; set; }

        [Required(ErrorMessage = "La obra social es obligatoria.")]
        [StringLength(100)]
        public required string ObraSocial { get; set; }

        internal PacienteResponse ToPacienteResponse()
        {
            return new PacienteResponse(this);
        }
    }
}
