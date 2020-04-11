using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GFOL.Data;
using GFOL.Models;

namespace GFOL.Repository
{
    public class SchemaRepository : IGenericRepository<Schema>
    {
        private readonly ApplicationDbContext _context;

        public SchemaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Schema> CreateAsync(Schema item)
        {
            _context.Schema.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var schema = _context.Schema.FirstOrDefault(x => x.Id == id);
            _context.Schema.Remove(schema ?? throw new InvalidOperationException());
            await _context.SaveChangesAsync();
        }

        public async Task<Schema>GetByIdAsync(int id)
        {
            var form = await Task.FromResult(_context.Schema.FirstOrDefault(x => x.Id == id));
            return form;
        }

        public async Task<IEnumerable<Schema>> FindByAsync(Expression<Func<Schema, bool>> predicate)
        {
            var forms = await Task.FromResult(_context.Schema.Where(predicate));
            return forms;
        }

        public async Task<Schema> UpdateAsync(int id, Schema item)
        {
            var schema = await Task.FromResult(_context.Schema.FirstOrDefault(x => x.Id == id));
            schema.SchemaJson = item.SchemaJson;
            schema.SavedDate=DateTime.Now;
            await _context.SaveChangesAsync();
            return schema;
        }

    }
}
