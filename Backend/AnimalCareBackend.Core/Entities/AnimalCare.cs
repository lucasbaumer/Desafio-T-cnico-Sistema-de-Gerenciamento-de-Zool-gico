using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Core.Entities
{
    public class AnimalCare
    {
        [Key]
        public Guid AnimalId { get; set; }
        public virtual Animal Animal { get; set; }

        public Guid CareId { get; set; }
        public virtual Care Care { get; set; }
    }
}
