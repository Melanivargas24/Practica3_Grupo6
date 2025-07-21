using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotacionObjetos.Models
{
    public partial class Votante
    {
        [Key]
        [Required]
        public string Cedula { get; set; }  

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public bool SiVoto { get; set; } = false;
    }
}
