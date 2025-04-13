using AnimalCareBackend.Application.DTOS;
using AnimalCareBackend.Application.Interface;
using AnimalCareBackend.Core.Entities;
using AnimalCareBackend.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Application.Service
{
    public class CareService : ICareService
    {
        private readonly ICareRepository _careRepository;
        private readonly IAnimalRepository _animalRepository;
       
        public CareService(ICareRepository careRepository, IAnimalRepository animalRepository)
        {
            _careRepository = careRepository;
            _animalRepository = animalRepository;
        }

        public async Task<Guid> AddCareAsync(CareCreateDto careDto)
        {
            var careId = Guid.NewGuid();

            var care = new Care
            {
                Id = careId,
                CareName = careDto.careName,
                Description = careDto.Description,
                Frequency = careDto.Frequency,
                AnimalCares = careDto.AnimalCares.Select(ac => new AnimalCare
                {
                    AnimalId = ac.AnimalId,
                    CareId = careId
                }).ToList()
            };

            await _careRepository.AddCareAsync(care);
            return care.Id;
        }

        public async Task<bool> DeleteCareAsync(Guid id)
        {
            await _careRepository.DeleteCare(id);
            return true;
        }

        public async Task<IEnumerable<CareWithAnimal>> GetAllCaresAsync()
        {
           var cares = await _careRepository.GetAllCaresAsync();

            var careWithAnimal = cares.Select(care => new CareWithAnimal
            {
                Id = care.Id,
                CareName = care.CareName,
                Description = care.Description,
                Frequency = care.Frequency,
                AnimalCares = care.AnimalCares.Select(AnimalCare => new AnimalCareDto
                {
                    AnimalId = AnimalCare.AnimalId,
                    AnimalName = _animalRepository.GetAnimalById(AnimalCare.AnimalId).Result.Name
                }).ToList()
            });

            return careWithAnimal;
        }

        public async Task<List<Care>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _careRepository.GetByIdsAsync(ids);
        }

        public async Task<Care> GetCareByIdAsync(Guid id)
        {
            return await _careRepository.GetCareByIdAsync(id);
        }

        public async Task<bool> UpdateCareAsync(Guid id, CareUpdateDto careDto)
        {
            var existingCare = await _careRepository.GetCareByIdAsync(id);

            if(existingCare == null)
            {
                throw new Exception("Cuidado não foi encontrado!");
            }

            existingCare.CareName = careDto.careName;
            existingCare.Description = careDto.Description;
            existingCare.Frequency = careDto.Frequency;
            existingCare.AnimalCares = careDto.AnimalCares;

            await _careRepository.UpdateCareAsync(existingCare);
            return true;

        }
    }
}
