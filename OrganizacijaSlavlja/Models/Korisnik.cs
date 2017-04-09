using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace OrganizacijaSlavlja.Models
{
    public class Korisnik
    {
        public int KorisnikID;
        public string ImePrezime;
        public string KorisnickoIme;
        public string Lozinka;
        public string Email;

        public Korisnik()
        {
        }

        public Korisnik(string ImePrezime, string KorisnickoIme, string Lozinka, string Email)
        {
            this.ImePrezime = ImePrezime;
            this.KorisnickoIme = KorisnickoIme;
            this.Lozinka = Lozinka;
            this.Email = Email;
        }

        public Korisnik(Korisnik k)
        {
            this.ImePrezime = k.ImePrezime;
            this.KorisnickoIme = k.KorisnickoIme;
            this.Lozinka = k.Lozinka;
            this.Email = k.Email;
        }
    }
}