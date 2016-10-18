using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class BusinessEntityWithContactsDto
    {
        public int BusinessEntityId { get; set; }
        public string ContactName { get; set; }
        public IEnumerable<string> Addresses { get; set; }
    }
}
