using AnimalCareBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Infrastructure.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAllAnimalsAsync();
        Task<Animal> GetAnimalById (Guid id);
        Task AddAnimalAsync (Animal animal);
        Task UpdateAnimalAsync (Animal animal);
        Task DeleteAnimalAsycn(Guid id);
        Task<Animal> GetAnimalWithCaresById(Guid id);
        void RemoveAnimalCares(IEnumerable<AnimalCare> animalCares);
        void AddAnimalCare(AnimalCare animalCare);
    }
}
