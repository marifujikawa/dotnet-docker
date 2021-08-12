using System;
using System.Collections.Generic;

#nullable disable

namespace teste.Models
{
    public partial class HeroPower
    {
        public int Id { get; set; }
        public int HeroId { get; set; }
        public int PowerId { get; set; }

        public virtual Hero Hero { get; set; }
        public virtual Power Power { get; set; }
    }
}
