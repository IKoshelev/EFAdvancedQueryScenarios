using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class JsonExtnesions
    {
        public static string ToJson(this object source)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(source);
        }
    }
}
