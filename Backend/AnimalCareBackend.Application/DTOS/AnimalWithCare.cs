using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Application.DTOS
{
    public class AnimalWithCare
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string birthDate { get; set; }
        public string Species { get; set; }
        public string Habitat { get; set; }
        public string CountryOfOrigin { get; set; }

        public List<CareResponseDto> Cares { get; set; } = new();
    }
}
