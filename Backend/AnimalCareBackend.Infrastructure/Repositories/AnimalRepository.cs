using AnimalCareBackend.Core.Entities;
using AnimalCareBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Infrastructure.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AppDbContext _context;

        public AnimalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAnimalAsync(Animal animal)
        {
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnimalAsycn(Guid id)
        {
           var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
        {
            return await _context.Animals
                .Include(a => a.AnimalCares)
                .ThenInclude(ac => ac.Care)
                .ToListAsync();
        }

        public async Task<Animal> GetAnimalById(Guid id)
        {
            return await _context.Animals
                .Include(a => a.AnimalCares)
                .ThenInclude(ac => ac.Care)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAnimalAsync(Animal animal)
        {
            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async Task<Animal> GetAnimalWithCaresById(Guid id)
        {
            return await _context.Animals
                .Include(a => a.AnimalCares)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void RemoveAnimalCares(IEnumerable<AnimalCare> animalCares)
        {
            _context.AnimalCares.RemoveRange(animalCares);
        }

        public void AddAnimalCare(AnimalCare animalCare)
        {
            _context.AnimalCares.Add(animalCare);
        }
    }
}
