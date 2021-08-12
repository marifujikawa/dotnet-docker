using System;
using System.Collections.Generic;

#nullable disable

namespace teste.Models
{
    public partial class Hero
    {
        public Hero()
        {
            HeroPowers = new HashSet<HeroPower>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HeroPower> HeroPowers { get; set; }
    }
}
