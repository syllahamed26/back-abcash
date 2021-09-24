using back_abcash.Contrats;
using back_abcash.Models;
using back_abcash.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Repositories
{
    public class CaisseRepository : GenericRepository<Caisse>, ICaisseRepository
    {
        private readonly AbcashDbContext _context;
        public CaisseRepository(AbcashDbContext context) : base(context) => _context = context;

        public async Task<IEnumerable<Caisse>> GetCaisseByLibelle(string libelle)
        {
            var result = await _context.Set<Caisse>().ToListAsync();
            var filterResult = result.FindAll(x => x.Libelle == libelle);

            return filterResult;
        }

        public async Task<bool> ExistingCaisse(int id)
        {
            var result = await _context.Set<Caisse>().ToListAsync();
            var filterResult = result.Any(x => x.Id == id);

            return filterResult;
        }
    }
}
