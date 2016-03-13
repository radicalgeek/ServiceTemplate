using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Models
{
    public class SampleEntity
    {
        public DateTime CreatedDate { get; set; }
        public string Id { get; set; }
        public decimal NewDecimalValue { get; set; }
        public Guid NewGuidValue { get; set; }
        public int NewIntValue { get; set; }
        public string NewStringValue { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
