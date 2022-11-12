using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Models
{
    class Partecipation
    {
        public long Id { get; set; }

        public long? IdAthlete { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public int? Age { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public string NOC { get; set; }

        public string Games { get; set; }

        public int? Year { get; set; }

        public string Season { get; set; }

        public string City { get; set; }

        public string Sport { get; set; }

        public string Event { get; set; }

        public string Medal { get; set; }
    }
}
