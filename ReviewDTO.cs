﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public class ReviewDTO
    {
        public string Titel { get; set; }
        public DateTime DatumTijd { get; }
        public GebruikerDTO Gebruiker { get; }
        public string StukTekst { get; set; }

        public ReviewDTO(string titel, string stuktekst, GebruikerDTO gebruiker, DateTime datumtijd)
        {
            this.Titel = titel;
            this.StukTekst = stuktekst;
            this.Gebruiker = gebruiker;
            this.DatumTijd = datumtijd;
        }
    }
}
