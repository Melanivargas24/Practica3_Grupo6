using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotacionObjetos.Models
{
    public partial class Partido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }


        public virtual ICollection<Voto> Votos { get; set; }
    }
}
