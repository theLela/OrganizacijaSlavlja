using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace OrganizacijaSlavlja.Models
{
    public class StavkaKonfiguracijeZurke
    {
        public Gost Gost;
        public Proizvod Proizvod;
        public float Kolicina;
        public Zurka Zurka;

        public StavkaKonfiguracijeZurke()
        {
            this.Gost = new Gost();
            this.Zurka = new Zurka();
            this.Proizvod = new Proizvod();
            this.Kolicina = 0;
        }

        public StavkaKonfiguracijeZurke(Gost Gost, Proizvod Proizvod, float Kolicina, Zurka Zurka)
        {
            this.Zurka = Zurka;
            this.Gost = Gost;
            this.Proizvod = Proizvod;
            this.Kolicina = Kolicina;
            
        }

        public StavkaKonfiguracijeZurke(StavkaKonfiguracijeZurke Stavka)
        {
            this.Zurka = new Zurka(Stavka.Zurka);
            this.Gost = new Gost(Stavka.Gost);
            this.Proizvod = new Proizvod(Stavka.Proizvod);
            this.Kolicina = Stavka.Kolicina;
        }

        public StavkaKonfiguracijeZurke(int ZurkaID, int GostID, int ProizvodID, float Kolicina)
        {
            this.Zurka = Broker.PronadjiZurku(ZurkaID);
            this.Gost = Broker.UzmiGosta(GostID);
            this.Proizvod = Broker.UzmiProizvod(ProizvodID);
            this.Kolicina = Kolicina;
        }

        public void PostaviZurku(int ZurkaID)
        {
            this.Zurka = Broker.PronadjiZurku(ZurkaID);
        }

        public void PostaviGosta(int GostID)
        {
            this.Gost = Broker.UzmiGosta(GostID);
        }

        public void PostaviProizvodKolicinu(int ProizvodID, float Kolicina)
        {
            this.Proizvod = Broker.UzmiProizvod(ProizvodID);
            this.Kolicina = Kolicina;
        }
    }
}