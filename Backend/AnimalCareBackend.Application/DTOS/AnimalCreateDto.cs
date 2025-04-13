using AnimalCareBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Application.DTOS
{
    public class AnimalCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Species { get; set; }
        public string Habitat { get; set; }
        public string CountryOfOrigin { get; set; }

        public List<Guid> careIds { get; set; }
    }
}
