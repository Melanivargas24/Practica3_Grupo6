using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotacionObjetos.ViewModels
{
    public partial class VotoViewModel
    {
        public int Id { get; set; }

        public string CedulaVotante { get; set; }

        public int PartidoId { get; set; }

        public DateTime Fecha { get; set; }

        public string NombreVotante { get; set; }
        public string NombrePartido { get; set; }
    }
}


