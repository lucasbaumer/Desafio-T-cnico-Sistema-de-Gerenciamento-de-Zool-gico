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
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ICareRepository _careRepository;
        
        public AnimalService(IAnimalRepository animalRepository, ICareRepository careRepository)
        {
            _animalRepository = animalRepository;
            _careRepository = careRepository;
        }

        public async Task<bool> DeleteAnimal(Guid id)
        {
            await _animalRepository.DeleteAnimalAsycn(id);
            return true;
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _animalRepository.GetAllAnimalsAsync();
        }

        public async Task<Animal> GetByIdAsync(Guid id)
        {
            return await _animalRepository.GetAnimalById(id);
        }

        public async Task<Guid> RegisterAnimal(AnimalCreateDto animalCreateDto)
        {
            var animalId = Guid.NewGuid();

            if (animalCreateDto.careIds != null && animalCreateDto.careIds.Any())
            {
                var existingCares = await _careRepository.GetByIdsAsync(animalCreateDto.careIds);

                if (existingCares.Count != animalCreateDto.careIds.Count)
                {
                    throw new Exception("Um ou mais cuidados informados não existem.");
                }
            }

            var animal = new Animal
            {
                Id = animalId,
                Name = animalCreateDto.Name,
                Description = animalCreateDto.Description,
                DateOfBirth = animalCreateDto.DateOfBirth,
                Species = animalCreateDto.Species,
                Habitat = animalCreateDto.Habitat,
                CountryOfOrigin = animalCreateDto.CountryOfOrigin,
                AnimalCares = animalCreateDto.careIds?.Select(careId => new AnimalCare
                {
                    AnimalId = animalId,
                    CareId = careId
                }).ToList() ?? new List<AnimalCare>()
            };

            await _animalRepository.AddAnimalAsync(animal);
            return animal.Id;
        }

        public async Task<bool> UpdateAnimal(Guid id, AnimalUpdateDto animalUpdateDto)
        {
            var animal = await _animalRepository.GetAnimalById(id);
            if(animal == null)
            {
                throw new Exception("Animal não foi encontrado");
            }

            animal.Name = animalUpdateDto.Name;
            animal.Description = animalUpdateDto.Description;
            animal.Habitat = animalUpdateDto.Habitat;
            animal.DateOfBirth = animalUpdateDto.BirthDate;
            animal.CountryOfOrigin = animalUpdateDto.CountryOfOrigin;

            animal.AnimalCares = animalUpdateDto.CareIds?.Select(careId => new AnimalCare
            {
                AnimalId = animal.Id,
                CareId = careId
            }).ToList() ?? new List<AnimalCare>();

            await _animalRepository.UpdateAnimalAsync(animal);
            return true;
        }
    }
}
