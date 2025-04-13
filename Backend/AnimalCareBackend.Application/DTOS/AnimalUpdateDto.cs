using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Application.DTOS
{
    public class AnimalUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Species { get; set; } = null!;
        public string Habitat { get; set; } = null!;
        public string CountryOfOrigin { get; set; } = null!;

        public List<Guid>? CareIds { get; set; }
    }
}
