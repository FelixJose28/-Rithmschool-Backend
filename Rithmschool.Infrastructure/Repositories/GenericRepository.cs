using Microsoft.EntityFrameworkCore;
using Rithmschool.Core.Interfaces;
using Rithmschool.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rithmschool.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RithmschoolContext _context;
        private readonly DbSet<T> _dbSetEntities;
        public GenericRepository(RithmschoolContext context)
        {
            _context = context;
            _dbSetEntities = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSetEntities.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            var entity = await _dbSetEntities.FindAsync(id);
            return entity;
        }
        public async Task<T> Add(T entity)
        {
            var entityEntry = await _dbSetEntities.AddAsync(entity);
            return entityEntry.Entity;

        }



        public async Task<T> Remove(int id)
        {
            var entity = await GetById(id);
            var entityEntry = _dbSetEntities.Remove(entity);
            return entityEntry.Entity;
        }

        public Task<T> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }
    }
}
