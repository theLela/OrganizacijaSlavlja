using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace OrganizacijaSlavlja.Models
{
    public class Prodavnica
    {
        public string Naziv;
        public int ProdavnicaID;

        public Prodavnica()
        {
        }

        public Prodavnica(string Naziv)
        {
            this.Naziv = Naziv;
        }

        public Prodavnica(Prodavnica pr)
        {
            this.ProdavnicaID = pr.ProdavnicaID;
            this.Naziv = pr.Naziv;
        }
    }
}