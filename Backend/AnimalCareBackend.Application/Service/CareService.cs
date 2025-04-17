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

            foreach(var animalCareDto in careDto.AnimalCares ?? new List<AnimalCareDto>())
            {
                var animal = await _animalRepository.GetAnimalById((Guid.Parse(animalCareDto.AnimalId)));
                if (animal == null)
                {
                    throw new Exception($"Animal com ID {animalCareDto.AnimalId} não encontrado!");
                }
            }

            var care = new Care
            {
                Id = careId,
                CareName = careDto.careName,
                Description = careDto.Description,
                Frequency = careDto.Frequency,
                AnimalCares = careDto.AnimalCares.Select(ac => new AnimalCare
                {
                    AnimalId = Guid.Parse(ac.AnimalId),
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
                    AnimalId = AnimalCare.AnimalId.ToString(),
                }).ToList()
            });

            return careWithAnimal;
        }

        public async Task<List<Care>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _careRepository.GetByIdsAsync(ids);
        }

        public async Task<CareWithAnimal> GetCareByIdAsync(Guid id)
        {
            var care = await _careRepository.GetCareByIdAsync(new List<Guid> { id });

            if(care == null || !care.Any())
            {
                throw new Exception($"Cuidado com Id {id} não encontrado!");
            }

            var careWithAnimal = new CareWithAnimal
            {
                Id = care.First().Id,
                CareName = care.First().CareName,
                Description = care.First().Description,
                Frequency = care.First().Frequency,
                AnimalCares = new List<AnimalCareDto>()
            };

            foreach(var animalCare in care.First().AnimalCares)
            {
                var animal = await _animalRepository.GetAnimalById(animalCare.AnimalId);
                if(animal != null)
                {
                    careWithAnimal.AnimalCares.Add(new AnimalCareDto
                    {
                        AnimalId = animalCare.AnimalId.ToString(),
                    });
                }
            }

            return careWithAnimal;
        }

        public async Task<CareUpdateDto> GetCareForUpdateAsync(Guid id)
        {
            var existingCare = await _careRepository.GetCareByIdAsync(new List<Guid>{ id });

            if(existingCare == null || !existingCare.Any())
            {
                return null;
            }

            var care = existingCare.First();


            return new CareUpdateDto
            {
                CareName = care.CareName,
                Description = care.Description,
                Frequency = care.Frequency,
                AnimalIds = care.AnimalCares.Select(ac => ac.AnimalId.ToString()).ToArray()
            };

        }

        public async Task<bool> UpdateCareAsync(Guid id, CareUpdateDto careUpdateDto)
        {
            var existingCare = await _careRepository.GetCareByIdAsync(new List<Guid> { id });

            foreach(var animalId in careUpdateDto.AnimalIds ?? Array.Empty<String>())
            {
                var animal = await _animalRepository.GetAnimalById(Guid.Parse(animalId));
                if(animal == null)
                {
                    throw new Exception($"animal com id: {animalId} não existe!");
                }
            }

            if (existingCare == null || !existingCare.Any())
            {
                return false;
            }

            var care = existingCare.First();

            care.CareName = careUpdateDto.CareName;
            care.Description = careUpdateDto.Description;
            care.Frequency = careUpdateDto.Frequency;

            await _careRepository.RemoveAnimalByCareAsync(care.Id);
            foreach (var animalId in careUpdateDto.AnimalIds ?? Array.Empty<String>())
            {
                care.AnimalCares.Add(new AnimalCare
                {
                    AnimalId = Guid.Parse(animalId),
                    CareId = care.Id
                });
            }
            

            await _careRepository.UpdateCareAsync(care);
            return true;
        }
    }
}
