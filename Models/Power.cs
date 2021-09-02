using System;
using System.Collections.Generic;
using Newtonsoft.Json;

#nullable disable

namespace teste.Models
{
    public class Power
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("heroes")]
        public ICollection<Hero> Heroes { get; set; }
    }
}
