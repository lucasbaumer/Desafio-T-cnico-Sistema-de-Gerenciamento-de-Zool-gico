using AnimalCareBackend.Core.Entities;
using AnimalCareBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Infrastructure.Repositories
{
    public class CareRepository : ICareRepository
    {
        private readonly AppDbContext _context;

        public CareRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCareAsync(Care care)
        {
            await _context.Cares.AddAsync(care);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCare(Guid id)
        {
            var care = await _context.Cares.FindAsync(id);
            if(care != null)
            {
                 _context.Cares.Remove(care);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Care>> GetAllCaresAsync()
        {
            return await _context.Cares
                .Include(c => c.AnimalCares)
                .ToListAsync();
        }

        public async Task<Care> GetCareByIdAsync(Guid id)
        {
            return await _context.Cares
                 .Include(c => c.AnimalCares)
                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCareAsync(Care care)
        {
            _context.Cares.Update(care);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Care>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Cares
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }
    }
}
