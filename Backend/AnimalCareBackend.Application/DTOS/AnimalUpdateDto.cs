using AnimalCareBackend.Application.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AnimalCareBackend.Application.DTOS
{
    public class AnimalUpdateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string BirthDate { get; set; } 
        public string Species { get; set; } = null!;
        public string Habitat { get; set; } = null!;
        public string CountryOfOrigin { get; set; } = null!;

    }
}
