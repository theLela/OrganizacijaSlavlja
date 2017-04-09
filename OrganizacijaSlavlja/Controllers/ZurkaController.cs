using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganizacijaSlavlja.Models;

namespace OrganizacijaSlavlja.Controllers
{
    public class ZurkaController : Controller
    {
        //
        // GET: /Zurka/

        public ActionResult Index()
        {
            //ViewBag.ListaZurki = Broker.VratiZurke();
            return View();

        }

        public ActionResult Dodaj()
        {
            ViewBag.ListaProdavnica = Broker.VratiProdavnice();
            return View();
        }

        [HttpPost]
        public ActionResult Dodaj(FormCollection fc)
        {
            Zurka z = new Zurka();
            z.Naziv = fc["Naziv"];
            z.Vrsta = fc["Vrsta"];
            try
            {
                z.VremeOdrzavanja = DateTime.Parse(fc["VremeOdrzavanja"]);
            }
            catch
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Neispravan datum." });
            }
            if ((z.Korisnik = Broker.UzmiKorisnika(fc["KorisnickoIme"])) == null)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Korisnik " + fc["KorisnickoIme"] + " ne postoji" });
            }
            z.Prodavnica = Broker.UzmiProdavnicu(Int32.Parse(fc["Prodavnica"]));

            if (Broker.SacuvajZurku(z) == false)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Nije uspelo cuvanje zurke." });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Pronadji()
        {
            return View();
        }

        public ActionResult Izmeni(FormCollection fc)
        {
            string KorisnickoIme = fc["KorisnickoIme"];
            string NazivZurke = fc["Naziv"];
            Zurka z;

            if ((z = Broker.PronadjiZurku(KorisnickoIme, NazivZurke)) == null)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Ne postoji zurka naziva " + NazivZurke + " , korisnika " + KorisnickoIme + "!" });
            }
            ViewBag.ListaProdavnica = Broker.VratiProdavnice();
            return View(z);
        }

        [HttpPost]
        public ActionResult Izmeni_2(FormCollection fc)
        {
            int ZurkaID = Int32.Parse(fc["ZurkaID"]);
            string KorisnickoIme = fc["KorisnickoIme"];
            string Naziv = fc["Naziv"];
            string Vrsta = fc["Vrsta"];
            DateTime VremeOdrzavanja;
            Zurka z;

            try
            {
                VremeOdrzavanja = DateTime.Parse(fc["VremeOdrzavanja"]);
            }
            catch
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Neispravan datum." });
            }

            int ProdavnicaID = Int32.Parse(fc["Prodavnica"]);

            if ((z = Broker.PronadjiZurku(ZurkaID)) == null)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Nepostojeca zurka" });
            }

            if (Broker.IzmeniZurku(z, Naziv, Vrsta, VremeOdrzavanja, ProdavnicaID) == false)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Neuspesna izmena zurke" });
            }

            return RedirectToAction("Index");
        }

        public ActionResult Greska(string PorukaGreske)
        {
            ViewBag.Poruka = PorukaGreske;
            return View();
        }

        public ActionResult Obrisi(FormCollection fc)
        {
            int ZurkaID = Int32.Parse(fc["ZurkaID"]);

            if (Broker.ObrisiZurku(ZurkaID) == false)
            {
                return RedirectToAction("Greska", new { PorukaGreske = "Neuspesno brisanje zurke" });
            }

            return RedirectToAction("Index");
        }
    }   
}
