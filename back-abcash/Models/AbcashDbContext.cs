using back_abcash.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Models
{
    public class AbcashDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Caisse> Caisses { get; set; }
        public DbSet<UserCaisse> UsersCaisses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public AbcashDbContext(DbContextOptions<AbcashDbContext> options) : base(options)
        {

        }

    }
}
