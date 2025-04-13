using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Core.Entities
{
    public class Care
    {
        public Guid Id { get; set; }
        public string careName { get; set; }
        public string Description { get; set; }
        public string Frequency { get; set; }

        public ICollection<AnimalCare> AnimalCares { get; set; }

    }
}
