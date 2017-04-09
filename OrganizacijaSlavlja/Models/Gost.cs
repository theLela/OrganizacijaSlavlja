using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace OrganizacijaSlavlja.Models
{
    public class Gost
    {
        public int GostID;
        public string Nadimak;

        public Gost()
        {
        }

        public Gost(int GostID, string Nadimak)
        {
            this.GostID = GostID;
            this.Nadimak = Nadimak;
        }

        public Gost(Gost g)
        {
            this.GostID = g.GostID;
            this.Nadimak = g.Nadimak;
        }
    }
}