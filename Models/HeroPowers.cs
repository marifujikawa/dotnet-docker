using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

#nullable disable

namespace teste.Models
{
    public class HeroPowers
    {
        public int HeroId { get; set; }
        public Hero Hero { get; set; }


        public int PowerId { get; set; }

        public Power Powers { get; set; }
    }
}
