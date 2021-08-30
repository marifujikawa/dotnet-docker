

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using teste.Models;

namespace teste.ViewModel
{
    public class HeroPowersViewModel
    {

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("powers")]
        public List<Power> Powers { get; set; }
    }
}