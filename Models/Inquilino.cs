using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpFinalLaboratorio.Net.Models
{
    public class Inquilino
    {
        [Key]
        public int IdInquilino { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string LugarTrabajo { get; set; } = string.Empty;
        public string NombreGarante { get; set; } = string.Empty;
        public string DniGarante { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Nueva propiedad para relacionar con el propietario
        [ForeignKey("Propietario")]
        public int PropietarioId { get; set; }
        public Propietario Propietario { get; set; }

        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
