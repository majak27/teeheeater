using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace teeheeater.Database
{
    public class Voorstellingen
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public string Beschrijving { get; set; }

        public string Foto { get; set; }

        public string Datum { get; set; }

        public string Tijd { get; set; }

        public int Ticketvoorraad { get; set; }
    }
}
