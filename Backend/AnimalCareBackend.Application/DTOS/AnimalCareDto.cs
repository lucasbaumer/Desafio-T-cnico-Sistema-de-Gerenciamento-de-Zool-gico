using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Application.DTOS
{
    public class AnimalCareDto
    {
        public Guid AnimalId { get; set; }
        public string AnimalName { get; set; }
    }
}
