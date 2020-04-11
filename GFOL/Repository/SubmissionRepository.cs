using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GFOL.Data;
using GFOL.Models;

namespace GFOL.Repository
{
    public class SubmissionRepository : IGenericRepository<Submission>
    {
        private readonly ApplicationDbContext _context;

        public SubmissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Submission> CreateAsync(Submission item)
        {
            _context.Submission.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var submission = _context.Submission.FirstOrDefault(x => x.Id == id);
            _context.Submission.Remove(submission ?? throw new InvalidOperationException());
            await _context.SaveChangesAsync();
        }

        public async Task<Submission> GetByIdAsync(int id)
        {
            var submission = await Task.FromResult(_context.Submission.FirstOrDefault(x => x.Id == id));
            return submission;
        }

        public async Task<IEnumerable<Submission>> FindByAsync(Expression<Func<Submission, bool>> predicate)
        {
            var submissions = await Task.FromResult(_context.Submission.Where(predicate));
            return submissions;
        }

        public async Task<Submission> UpdateAsync(int id, Submission item)
        {
            var submission = await Task.FromResult(_context.Submission.FirstOrDefault(x => x.Id == id));
            submission.SubmissionJson = item.SubmissionJson;
            submission.SavedDate=DateTime.Now;
            await _context.SaveChangesAsync();
            return submission;
        }
    }
}
