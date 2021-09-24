using back_abcash.Contrats;
using back_abcash.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AbcashDbContext _context;
        private DbSet<T> connection = null;
        public GenericRepository(AbcashDbContext context)
        {
            _context = context;
            connection = _context.Set<T>();

        }

        public int Complete() => _context.SaveChanges();
        

        public void Delete(int id)
        {
            T existing = connection.Find(id);
            connection.Remove(existing);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await connection.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await connection.FindAsync(id);
        }

        public async Task<T> Insert(T obj)
        {
            await connection.AddAsync(obj);
            return obj;
        }

        public void Update(T obj)
        {
            connection.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

    }

}
