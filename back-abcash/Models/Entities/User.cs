using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Models.Entities
{
    [Table("Users")]
    public class User : BaseModel
    {
        [Required]
        public String Nom { get; set; }
        [Required]
        public String Prenoms { get; set; }
        [Required]
        public String Contact { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Login { get; set; }
        [Required]
        [MinLength(8)]
        public String Password { get; set; }
        public Boolean Statut { get; set; }
        [Required]
        public int RoleId { get; set; }
        public User()
        {
            this.Statut = true;
        }
    }
}
