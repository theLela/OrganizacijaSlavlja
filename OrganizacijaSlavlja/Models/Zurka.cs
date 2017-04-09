using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace OrganizacijaSlavlja.Models
{
    public class Zurka
    {
        public int ZurkaID;
        public string Naziv;
        public string Vrsta;
        public DateTime VremeOdrzavanja;
        public Korisnik Korisnik;
        public Prodavnica Prodavnica;

        public Zurka()
        {
        }

        public Zurka(string Naziv, string Vrsta, DateTime VremeOdrzavanja, Korisnik Korisnik, Prodavnica Prodavnica)
        {
            this.Naziv = Naziv;
            this.Vrsta = Vrsta;
            this.VremeOdrzavanja = VremeOdrzavanja;
            this.Korisnik = Korisnik;
            this.Prodavnica = Prodavnica;
        }

        public Zurka(Zurka z)
        {
            this.ZurkaID = z.ZurkaID;
            this.Naziv = z.Naziv;
            this.Vrsta = z.Vrsta;
            this.VremeOdrzavanja = z.VremeOdrzavanja;
            this.Korisnik = new Korisnik(z.Korisnik);
            this.Prodavnica = new Prodavnica(z.Prodavnica);
        }
    }
}