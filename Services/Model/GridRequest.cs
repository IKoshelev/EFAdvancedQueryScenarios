using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class GridRequest
    {
        public IQueryable<T> WrapQuery<T>(IQueryable<T> initialQuery)
        {
            return initialQuery;
        }
    }

    public class GridRequestWithAdditionalPayload<T> : GridRequest
    {
        public T Payload { get; set; }
    }

    public class TextSearchPayload
    {
        public string TextSearch { get; set; }
    }
}
