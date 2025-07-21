using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotacionObjetos.ViewModels
{
    public partial class VotanteViewModel
    {
        public string Cedula { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public bool SiVoto { get; set; } = false;

        public int? VotoId { get; set; }

        public int? PartidoIdVotado { get; set; }

        public DateTime? FechaVoto { get; set; }
    }
}
