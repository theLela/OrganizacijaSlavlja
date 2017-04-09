using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace OrganizacijaSlavlja.Models
{
    public class Proizvod
    {
        public int ProizvodID;
        public string Naziv;
        public string Kategorija;

        public Proizvod()
        {
        }

        public Proizvod(int ProizvodID, string Naziv, string Kategorija)
        {
            this.ProizvodID = ProizvodID;
            this.Naziv = Naziv;
            this.Kategorija = Kategorija;
        }

        public Proizvod(Proizvod p)
        {
            this.ProizvodID = p.ProizvodID;
            this.Naziv = p.Naziv;
            this.Kategorija = p.Kategorija;
        }

    }
}