using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganizacijaSlavlja.Models;


namespace OrganizacijaSlavlja.Controllers
{
    public class KonfiguracijaController : Controller
    {
        //
        // GET: /Konfiguracija/

        public static StavkaKonfiguracijeZurke Stavka = new StavkaKonfiguracijeZurke();
        public static List<StavkaKonfiguracijeZurke> ListaStavki = new List<StavkaKonfiguracijeZurke>();

        public ActionResult Index()
        {
            ViewBag.SveStavke = Broker.VratiKonfiguracije();
            return View();
        }

        public ActionResult Dodaj()
        {
            ViewBag.ListaZurki = Broker.VratiZurke();
            ViewBag.ListaGostiju = Broker.VratiGoste();
            ViewBag.ListaProizvoda = Broker.VratiProizvode();
            ViewBag.ListaStavki = ListaStavki;
            return View(Stavka);
        }

        public ActionResult UbaciZurku(FormCollection  fc)
        {
            int ZurkaID = Int32.Parse(fc["ZurkaID"]);
            Stavka.PostaviZurku(ZurkaID);
            return RedirectToAction("Dodaj");
        }

        public ActionResult UbaciGosta(FormCollection fc)
        {
            int GostID = Int32.Parse(fc["GostID"]);
            Stavka.PostaviGosta(GostID);
            return RedirectToAction("Dodaj");
        }

        public ActionResult UbaciProizvodKolicinu(FormCollection fc)
        {
            int ProizvodID = Int32.Parse(fc["ProizvodID"]);
            float Kolicina = float.Parse(fc["Kolicina"]);
            Stavka.PostaviProizvodKolicinu(ProizvodID, Kolicina);

            ListaStavki.Add(new StavkaKonfiguracijeZurke(Stavka));
            return RedirectToAction("Dodaj");
        }

        public ActionResult SacuvajListu()
        {
            for (int i = 0; i < ListaStavki.Count; i++)
            {
                if (Broker.SacuvajStavkuKonfiguracije(ListaStavki[i]) == false)
                {
                    return RedirectToAction("Greska", new { PorukaGreske = "Cuvanje liste nije uspelo." });
                }
            }
            ListaStavki.Clear();
            Stavka = new StavkaKonfiguracijeZurke();
            return RedirectToAction("Dodaj");
        }

        public ActionResult Pronadji()
        {
            ViewBag.ListaZurki = Broker.VratiZurke();
            ViewBag.ListaGostiju = Broker.VratiGoste();
            return View();
        }

        public ActionResult Izmeni(FormCollection fc)
        {
            int ZurkaID = Int32.Parse(fc["ZurkaID"]);
            int GostID = Int32.Parse(fc["GostID"]);
            ViewBag.ListaStavki = Broker.PronadjiStavkeKonfiguracije(ZurkaID, GostID);
            ViewBag.ListaProizvoda = Broker.VratiProizvode();
            ViewBag.Zurka = Broker.PronadjiZurku(ZurkaID);
            ViewBag.Gost = Broker.UzmiGosta(GostID);
            return View();
        }

        
        public ActionResult Izmeni2(FormCollection fc)
        {
            int ZurkaID = Int32.Parse(fc["ZurkaID"]);
            int GostID = Int32.Parse(fc["GostID"]);
            int ProizvodID = Int32.Parse(fc["ProizvodID"]);
            float Kolicina = float.Parse(fc["Kolicina"]);

            StavkaKonfiguracijeZurke stavka = Broker.PronadjiStavkuKonfiguracije(ZurkaID, GostID, ProizvodID);
            if (Broker.IzmeniStavkuKonfiguracije(stavka, Kolicina) == false)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Izmena stavke nije uspela." });
            }
            return RedirectToAction("Pronadji");
        }

        public ActionResult DodajStavku(FormCollection fc)
        {
            int ZurkaID = Int32.Parse(fc["ZurkaID"]);
            int GostID = Int32.Parse(fc["GostID"]);
            int ProizvodID = Int32.Parse(fc["ProizvodID"]);
            float Kolicina = float.Parse(fc["Kolicina"]);

            StavkaKonfiguracijeZurke stavka = new StavkaKonfiguracijeZurke(ZurkaID, GostID, ProizvodID, Kolicina);
            if (Broker.SacuvajStavkuKonfiguracije(stavka) == false)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Cuvanje stavke nije uspelo." });
            }
            return RedirectToAction("Pronadji");
        }

        public ActionResult ObrisiStavku(FormCollection fc)
        {
            int ZurkaID = Int32.Parse(fc["ZurkaID"]);
            int GostID = Int32.Parse(fc["GostID"]);
            int ProizvodID = Int32.Parse(fc["ProizvodID"]);

            StavkaKonfiguracijeZurke stavka = new StavkaKonfiguracijeZurke(ZurkaID, GostID, ProizvodID, 0);
            if (Broker.ObrisiStavkuKonfiguracije(stavka) == false)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Brisanje stavke nije uspelo." });
            }
            return RedirectToAction("Pronadji");
        }

        public ActionResult Greska(string PorukaGreske)
        {
            ViewBag.Poruka = PorukaGreske;
            return View();
        }

        public ActionResult ObrisiKonfiguraciju()
        {
            ViewBag.ListaZurki = Broker.VratiZurke();
            return View();
        }

        [HttpPost]
        public ActionResult ObrisiKonfiguraciju(FormCollection fc)
        {
            int ZurkaID = Int32.Parse(fc["ZurkaID"]);

            if (Broker.ObrisiKonfiguraciju(ZurkaID) == false)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Neuspesno brisanje konfiguracije" });
            }

            return RedirectToAction("Index");
        }
    }    
}
