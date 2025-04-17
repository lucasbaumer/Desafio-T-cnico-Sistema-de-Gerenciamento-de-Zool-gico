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
            var care = await _context.Cares.Include(c => c.AnimalCares).FirstOrDefaultAsync(c => c.Id == id);
            if(care == null)
            {
                throw new Exception($"Não foi possivel encontrar o animal com id: {id}");
            }

            if (care.AnimalCares.Any())
            {
                throw new Exception("Não é possivel excluir o cuidado pois está associado a um ou mais animais");
            }
            _context.Cares.Remove(care);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAnimalByCareAsync(Guid careId)
        {
            var existingAnimalCares = await _context.AnimalCares
                .Where(ac => ac.CareId == careId)
                .ToListAsync();

            _context.AnimalCares.RemoveRange(existingAnimalCares);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Care>> GetAllCaresAsync()
        {
            return await _context.Cares
                .Include(c => c.AnimalCares)
                .ToListAsync();
        }

        public async Task<List<Care>> GetCareByIdAsync(List<Guid> id)
        {
            return await _context.Cares
                .Where(c => id.Contains(c.Id))
                .Include(c => c.AnimalCares)
                .ToListAsync();
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
