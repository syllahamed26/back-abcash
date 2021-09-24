using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Contrats
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T obj);
        void Update(T obj);
        void Delete(int id);
        int Complete();
    }

}
