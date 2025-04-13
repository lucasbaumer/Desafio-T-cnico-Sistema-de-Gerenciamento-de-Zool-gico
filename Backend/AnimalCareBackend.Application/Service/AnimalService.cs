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
                DateOfBirth = animal.DateOfBirth,
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
                DateOfBirth = animal.DateOfBirth,
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
            var animal = await _animalRepository.GetAnimalWithCaresById(id); // Inclui os cuidados
            if (animal == null)
                throw new Exception("Animal não foi encontrado");

            // Atualiza os campos simples
            animal.Name = animalUpdateDto.Name;
            animal.Description = animalUpdateDto.Description;
            animal.Habitat = animalUpdateDto.Habitat;
            animal.DateOfBirth = animalUpdateDto.BirthDate; // Verifique se o campo está sendo mapeado corretamente
            animal.CountryOfOrigin = animalUpdateDto.CountryOfOrigin;

            // Limpa a lista atual de cuidados ANTES de adicionar os novos
            if (animalUpdateDto.CareIds != null)
            {
                animal.AnimalCares.Clear(); // Limpa a lista de cuidados existentes, se houver

                // Se os CareIds não forem nulos ou vazios, adiciona os novos cuidados
                if (animalUpdateDto.CareIds.Any())
                {
                    foreach (var careId in animalUpdateDto.CareIds)
                    {
                        var animalCare = new AnimalCare
                        {
                            AnimalId = animal.Id,
                            CareId = careId
                        };
                        animal.AnimalCares.Add(animalCare);
                    }
                }
            }

            // Verifique se a lista de cuidados não está vazia, mas sem lançar exceção
            // O animal pode ter cuidados ou não, e não é obrigatório
            await _animalRepository.UpdateAnimalAsync(animal);
            return true;
        }

    }
}