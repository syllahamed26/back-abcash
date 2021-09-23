using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Models.Entities
{
    [Table("Sessions")]
    public class Session : BaseModel
    {
        [Required]
        public String CodeSession { get; set; }
        [Required]
        public Boolean Statut { get; set; }
        [Required]
        public int CaisseId { get; set; }
        [Required]
        public int UserId { get; set; }

        public Session()
        {
            this.Statut = true;
        }
    }
}
