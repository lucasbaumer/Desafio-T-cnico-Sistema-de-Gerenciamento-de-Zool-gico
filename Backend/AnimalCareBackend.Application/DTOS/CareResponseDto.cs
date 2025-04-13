using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Application.DTOS
{
    public class CareResponseDto
    {
        public Guid Id { get; set; }
        public string CareName { get; set; }
        public string Description { get; set; }
        public string Frequency { get; set; }
    }
}
