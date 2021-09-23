using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Models.Entities
{
    [Table("UsersCaisses")]
    public class UserCaisse : BaseModel
    {
        public DateTime DateAffectation { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CaisseId { get; set; }
    }
}
