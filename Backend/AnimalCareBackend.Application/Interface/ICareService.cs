using AnimalCareBackend.Application.DTOS;
using AnimalCareBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Application.Interface
{
    public interface ICareService
    {
        Task<IEnumerable<CareWithAnimal>> GetAllCaresAsync();
        Task<CareWithAnimal> GetCareByIdAsync(Guid id);
        Task<Guid> AddCareAsync(CareCreateDto careDto);
        Task<CareUpdateDto> GetCareForUpdateAsync(Guid id);
        Task<bool> UpdateCareAsync(Guid id, CareUpdateDto careUpdateDto);
        Task<bool> DeleteCareAsync(Guid id);
        Task<List<Care>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}
