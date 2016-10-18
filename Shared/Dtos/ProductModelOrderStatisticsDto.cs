using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductModelOrderStatisticsDto
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public double AverageDuration { get; set; }
        public double AverageRoutings { get; set; }
    }
}
