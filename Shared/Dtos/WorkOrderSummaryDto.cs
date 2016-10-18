using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class WorkOrderSummaryDto
    {
        public int ProductModelId { get; set; }
        public string ModelName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OrderId { get; set; }
        public int? DurationDays { get; set; }
        public int RoutingsCount { get; set; }
        public IEnumerable<string> Locations { get; set; } 
    }
}
