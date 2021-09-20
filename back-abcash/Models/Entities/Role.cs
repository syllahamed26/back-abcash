using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Models.Entities
{
    [Table("Roles")]
    public class Role :  BaseModel
    {
        [Required]
        public string Libelle { get; set; }
    }
}
