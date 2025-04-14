using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AnimalCareBackend.Core.Entities
{
    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string DateOfBirth { get; set; }
        public string Species { get; set; }
        public string Habitat {  get; set; }
        public string CountryOfOrigin { get; set; }

        public ICollection<AnimalCare> AnimalCares { get; set; } = new List<AnimalCare>();
    }
}
