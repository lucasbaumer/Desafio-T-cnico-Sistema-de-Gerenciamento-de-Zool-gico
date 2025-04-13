using AnimalCareBackend.Application.DTOS;
using AnimalCareBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Application.Interface
{
    public interface IAnimalService
    {
        Task<IEnumerable<AnimalWithCare>> GetAllAsync();
        Task<AnimalWithCare> GetByIdAsync(Guid id);
        Task<Guid> RegisterAnimal(AnimalCreateDto animalCreateDto);
        Task<bool> UpdateAnimal(Guid id, AnimalUpdateDto animalUpdateDto);
        Task<bool> DeleteAnimal(Guid id);

    }
}
