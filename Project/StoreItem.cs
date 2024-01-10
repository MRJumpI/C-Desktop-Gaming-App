using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class StoreItem
    {
        [JsonProperty("store")]
        public Store Store { get; set; }
    }
}
