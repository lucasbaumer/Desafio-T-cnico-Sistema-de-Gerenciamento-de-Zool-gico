using AnimalCareBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Infrastructure.Repositories
{
    public interface ICareRepository
    {
        Task<IEnumerable<Care>> GetAllCaresAsync();
        Task<List<Care>> GetCareByIdAsync(List<Guid> id);
        Task AddCareAsync(Care care);
        Task UpdateCareAsync(Care care);
        Task DeleteCare(Guid id);
        Task RemoveAnimalByCareAsync(Guid careId);
        Task<List<Care>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}
