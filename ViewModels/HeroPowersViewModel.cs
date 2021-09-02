

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using teste.Models;

namespace teste.ViewModel
{
    public class HeroPowersViewModel
    {
        [JsonProperty("hero")]
        public Hero Hero { get; set; }

        [JsonProperty("powers")]
        public List<Power> Powers { get; set; }
    }
}