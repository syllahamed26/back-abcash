using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Models.Entities
{
    [Table("Caisses")]
    public class Caisse : BaseModel
    {
        [Required]
        public String Libelle { get; set; }
        [Required]
        public String Emplacement { get; set; }
        public Boolean Statut { get; set; }

        public Caisse()
        {
            this.Statut = true;
        }
    }
}
