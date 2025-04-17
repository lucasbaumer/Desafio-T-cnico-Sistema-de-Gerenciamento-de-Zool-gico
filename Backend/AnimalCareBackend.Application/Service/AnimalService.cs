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

        public async Task<IEnumerable<AnimalWithCare>> GetAllAsync()
        {
            var animals = await _animalRepository.GetAllAnimalsAsync();
            var animalsDto = animals.Select(animal => new AnimalWithCare
            {
                Id = animal.Id,
                Name = animal.Name,
                Description = animal.Description,
                birthDate = animal.birthDate,
                Species = animal.Species,
                Habitat = animal.Habitat,
                CountryOfOrigin = animal.CountryOfOrigin,
                Cares = animal.AnimalCares
                    .Where(ac => ac.Care != null)
                    .Select(ac => new CareResponseDto
                    {
                        Id = ac.Care.Id,
                        CareName = ac.Care.CareName,
                        Description = ac.Care.Description,
                        Frequency = ac.Care.Frequency
                    }).ToList()
            }).ToList();

            return animalsDto;
        }

        public async Task<AnimalWithCare> GetByIdAsync(Guid id)
        {
            var animal = await _animalRepository.GetAnimalById(id);

            if (animal == null)
            {
                return null;
            }

            var dto = new AnimalWithCare
            {
                Id = animal.Id,
                Name = animal.Name,
                Description = animal.Description,
                birthDate = animal.birthDate,
                Species = animal.Species,
                Habitat = animal.Habitat,
                CountryOfOrigin = animal.CountryOfOrigin,
                Cares = animal.AnimalCares
                    .Where(ac => ac.Care != null)
                    .Select(ac => new CareResponseDto
                    {
                        Id = ac.Care.Id,
                        CareName = ac.Care.CareName,
                        Description = ac.Care.Description,
                        Frequency = ac.Care.Frequency
                    }).ToList()
            };

            return dto;
        }

        public async Task<Guid> RegisterAnimal(AnimalCreateDto animalCreateDto)
        {
            var animalId = Guid.NewGuid();


            var animal = new Animal
            {
                Id = animalId,
                Name = animalCreateDto.Name,
                Description = animalCreateDto.Description,
                birthDate = animalCreateDto.birthDate,
                Species = animalCreateDto.Species,
                Habitat = animalCreateDto.Habitat,
                CountryOfOrigin = animalCreateDto.CountryOfOrigin,

            };

            await _animalRepository.AddAnimalAsync(animal);
            return animal.Id;
        }

        public async Task<bool> UpdateAnimal(Guid id, AnimalUpdateDto animalUpdateDto)
        {
            var animal = await _animalRepository.GetAnimalWithCaresById(id);
            if (animal == null)
                throw new Exception("Animal não foi encontrado");

            animal.Name = animalUpdateDto.Name;
            animal.Description = animalUpdateDto.Description;
            animal.Habitat = animalUpdateDto.Habitat;
            animal.birthDate = animalUpdateDto.BirthDate;
            animal.CountryOfOrigin = animalUpdateDto.CountryOfOrigin;

            await _animalRepository.UpdateAnimalAsync(animal);
            return true;
        }

    }
}