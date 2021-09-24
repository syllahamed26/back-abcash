using back_abcash.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Contrats
{
    public interface ICaisseRepository : IGenericRepository<Caisse>
    {
        Task<IEnumerable<Caisse>> GetCaisseByLibelle(string libelle);
        Task<bool> ExistingCaisse(int id);

    }

}
