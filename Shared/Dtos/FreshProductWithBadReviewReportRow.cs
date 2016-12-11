using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class FreshProductWithBadReviewReportRow
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public bool IsFullyAsembledBike { get; set; }
        public double AverageReviewScore { get; set; }
        public bool IsAverageReviewScroePositive { get; set; }
    }
}
