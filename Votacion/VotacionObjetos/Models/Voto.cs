using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotacionObjetos.Models
{
    public partial class Voto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CedulaVotante { get; set; }

        [ForeignKey("CedulaVotante")]
        public virtual Votante Votante { get; set; }

        [Required]
        public int PartidoId { get; set; }

        [ForeignKey("PartidoId")]
        public virtual Partido Partido { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }

}
