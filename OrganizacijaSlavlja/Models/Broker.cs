using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace OrganizacijaSlavlja.Models
{
    public class Broker
    {
        //KORISNIK
        public static Korisnik UzmiKorisnika(string KorisnickoIme)
        {
            Korisnik korisnik = null;
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();

                string upit = "Select * from Korisnik where KorisnickoIme=@KorisnickoIme";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@KorisnickoIme", KorisnickoIme);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        korisnik = new Korisnik();
                        korisnik.KorisnikID = Int32.Parse(citac["KorisnikID"].ToString());
                        korisnik.KorisnickoIme = citac["KorisnickoIme"].ToString();
                        korisnik.ImePrezime = citac["ImePrezime"].ToString();
                        korisnik.Lozinka = citac["Lozinka"].ToString();
                        korisnik.Email = citac["Email"].ToString();
                        break;
                    }
                }
                konekcija.Close();
            }
            return korisnik;
        }

        public static Korisnik UzmiKorisnika(int KorisnikID)
        {
            Korisnik korisnik = null;
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Korisnik where KorisnikID=@KorisnikID";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@KorisnikID", KorisnikID);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        korisnik = new Korisnik();
                        korisnik.KorisnikID = Int32.Parse(citac["KorisnikID"].ToString());
                        korisnik.KorisnickoIme = citac["KorisnickoIme"].ToString();
                        korisnik.ImePrezime = citac["ImePrezime"].ToString();
                        korisnik.Lozinka = citac["Lozinka"].ToString();
                        korisnik.Email = citac["Email"].ToString();
                        break;
                    }
                }

                konekcija.Close();
            }
            return korisnik;
        }

        //PRODAVNICA
        public static Prodavnica UzmiProdavnicu(int ID)
        {
            Prodavnica prodavnica = null;
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Prodavnica where ProdavnicaID=@Parametar";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@Parametar", ID);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        prodavnica = new Prodavnica();
                        prodavnica.ProdavnicaID = Int32.Parse(citac["ProdavnicaID"].ToString());
                        prodavnica.Naziv = citac["Naziv"].ToString();
                        break;
                    }
                }
                konekcija.Close();
            }
            return prodavnica;
        }

        public static List<Prodavnica> VratiProdavnice()
        {
            List<Prodavnica> lista = new List<Prodavnica>();

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Prodavnica";
                SqlCommand komanda = new SqlCommand(upit, konekcija);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        Prodavnica prodavnica = new Prodavnica();
                        prodavnica.Naziv = citac["Naziv"].ToString();
                        prodavnica.ProdavnicaID = Int32.Parse(citac["ProdavnicaID"].ToString());
                        lista.Add(prodavnica);
                    }
                }
                konekcija.Close();
            }
            return lista;
        }

        //ZURKA
        public static bool SacuvajZurku(Zurka zurka)
        {
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                try
                {

                    SqlCommand komanda = new SqlCommand("dbo.DodajZurku", konekcija);
                    komanda.CommandType = CommandType.StoredProcedure;

                    komanda.Parameters.AddWithValue("@Naziv", zurka.Naziv);
                    komanda.Parameters.AddWithValue("@Vrsta", zurka.Vrsta);
                    komanda.Parameters.AddWithValue("@VremeOdrzavanja", zurka.VremeOdrzavanja);
                    komanda.Parameters.AddWithValue("@KorisnikID", zurka.Korisnik.KorisnikID);
                    komanda.Parameters.AddWithValue("@ProdavnicaID", zurka.Prodavnica.ProdavnicaID);

                    komanda.ExecuteNonQuery();
                    konekcija.Close();
                    return true;
                }
                catch (Exception e)
                {
                    konekcija.Close();
                    return false;
                }
            }
        }

        public static Zurka PronadjiZurku(string KorisnickoIme, string NazivZurke)
        {
            Zurka zurka = null;

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();

                if (Broker.UzmiKorisnika(KorisnickoIme) == null)
                    return null;

                int KorisnikID = Broker.UzmiKorisnika(KorisnickoIme).KorisnikID;

                string upit = "Select * from Zurka where KorisnikID=@KorisnikID and Naziv=@Naziv";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@KorisnikID", KorisnikID);
                komanda.Parameters.AddWithValue("@Naziv", NazivZurke);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        zurka = new Zurka();
                        zurka.Korisnik = Broker.UzmiKorisnika(KorisnickoIme);
                        zurka.Naziv = NazivZurke;
                        zurka.ZurkaID = Int32.Parse(citac["ZurkaID"].ToString());
                        zurka.VremeOdrzavanja = DateTime.Parse(citac["VremeOdrzavanja"].ToString());
                        zurka.Vrsta = citac["Vrsta"].ToString();
                        zurka.Prodavnica = Broker.UzmiProdavnicu(Int32.Parse(citac["ProdavnicaID"].ToString()));
                        break;
                    }
                }
                konekcija.Close();
            }
            return zurka;
        }

        public static bool IzmeniZurku(Zurka zurka, string Naziv, string Vrsta, DateTime VremeOdrzavanja, int ProdavnicaID)
        {
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                try
                {

                    SqlCommand komanda = new SqlCommand("dbo.IzmeniZurku", konekcija);
                    komanda.CommandType = CommandType.StoredProcedure;

                    komanda.Parameters.AddWithValue("@Naziv", Naziv);
                    komanda.Parameters.AddWithValue("@Vrsta", Vrsta);
                    komanda.Parameters.AddWithValue("@VremeOdrzavanja", VremeOdrzavanja);
                    komanda.Parameters.AddWithValue("@ProdavnicaID", ProdavnicaID);
                    komanda.Parameters.AddWithValue("@ZurkaID", zurka.ZurkaID);

                    komanda.ExecuteNonQuery();
                    konekcija.Close();
                    return true;
                }
                catch (Exception e)
                {
                    konekcija.Close();
                    return false;
                }
            }
        }

        public static Zurka PronadjiZurku(int ZurkaID)
        {
            Zurka zurka = null;

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select Z.*, K.KorisnickoIme from Zurka Z JOIN Korisnik K on Z.KorisnikID=K.KorisnikID where Z.ZurkaID=@ZurkaID ";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@ZurkaID", ZurkaID);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        zurka = new Zurka();
                        zurka.ZurkaID = Int32.Parse(citac["ZurkaID"].ToString());
                        zurka.Naziv = citac["Naziv"].ToString();
                        zurka.VremeOdrzavanja = DateTime.Parse(citac["VremeOdrzavanja"].ToString());
                        zurka.Vrsta = citac["Vrsta"].ToString();
                        zurka.Prodavnica = Broker.UzmiProdavnicu(Int32.Parse(citac["ProdavnicaID"].ToString()));
                        zurka.Korisnik = Broker.UzmiKorisnika(citac["KorisnickoIme"].ToString());
                        break;
                    }
                }
                konekcija.Close();
            }
            return zurka;
        }

        public static bool ObrisiZurku(int ZurkaID)
        {
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                try
                {

                    SqlCommand komanda = new SqlCommand("dbo.ObrisiZurku", konekcija);
                    komanda.CommandType = CommandType.StoredProcedure;

                    komanda.Parameters.AddWithValue("@ZurkaID", ZurkaID);
                  
                    komanda.ExecuteNonQuery();
                    konekcija.Close();
                    return true;
                }
                catch (Exception e)
                {
                    konekcija.Close();
                    return false;
                }
            }
        }

        public static List<Zurka> VratiZurke()
        {

            List<Zurka> lista = new List<Zurka>();

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Zurka";
                SqlCommand komanda = new SqlCommand(upit, konekcija);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        Zurka zurka = new Zurka();
                        zurka.ZurkaID = Int32.Parse(citac["ZurkaID"].ToString());
                        zurka.Naziv = citac["Naziv"].ToString();
                        zurka.VremeOdrzavanja = DateTime.Parse(citac["VremeOdrzavanja"].ToString());
                        zurka.Vrsta = citac["Vrsta"].ToString();
                        zurka.Korisnik = Broker.UzmiKorisnika(Int32.Parse(citac["KorisnikID"].ToString()));
                        zurka.Prodavnica = Broker.UzmiProdavnicu(Int32.Parse(citac["ProdavnicaID"].ToString()));
                        lista.Add(zurka);
                    }
                }
                konekcija.Close();
            }
            return lista;
        }

        //GOST
         public static Gost UzmiGosta(string Nadimak)
        {
            Gost gost = null;

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Gost where Nadimak=@Nadimak";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@Nadimak", Nadimak);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        gost = new Gost();
                        gost.GostID = Int32.Parse(citac["GostID"].ToString());
                        gost.Nadimak = citac["Nadimak"].ToString();
                        break;
                    }
                }
                konekcija.Close();
            }
            return gost;
        }

        public static Gost UzmiGosta(int GostID)
        {
            Gost gost = null;

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Gost where GostID=@ID";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@ID", GostID);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        gost = new Gost();
                        gost.GostID = Int32.Parse(citac["GostID"].ToString());
                        gost.Nadimak = citac["Nadimak"].ToString();
                        break;
                    }
                }
                konekcija.Close();
            }
            return gost;
        }

        public static List<Gost> VratiGoste()
        {
            List<Gost> lista = new List<Gost>();

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Gost";
                SqlCommand komanda = new SqlCommand(upit, konekcija);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        Gost gost = new Gost();
                        gost.GostID = Int32.Parse(citac["GostID"].ToString());
                        gost.Nadimak = citac["Nadimak"].ToString();
                        lista.Add(gost);
                    }
                }
                konekcija.Close();
            }
            return lista;
        }

        //PROIZVOD
        public static Proizvod UzmiProizvod(string Naziv)
        {
            Proizvod proizvod = null;

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Proizvod where Naziv=@Naziv";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@Naziv", Naziv);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        proizvod = new Proizvod();
                        proizvod.ProizvodID = Int32.Parse(citac["ProzvodID"].ToString());
                        proizvod.Naziv = citac["Naziv"].ToString();
                        proizvod.Kategorija = citac["Kategorija"].ToString();
                        break;
                    }
                }
                konekcija.Close();
            }
            return proizvod;
        }

        public static Proizvod UzmiProizvod(int ProizvodID)
        {
            Proizvod proizvod = null;

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Proizvod where ProizvodID=@ID";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@ID", ProizvodID);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        proizvod = new Proizvod();
                        proizvod.ProizvodID = Int32.Parse(citac["ProizvodID"].ToString());
                        proizvod.Naziv = citac["Naziv"].ToString();
                        proizvod.Kategorija = citac["Kategorija"].ToString();
                        break;
                    }
                }
                konekcija.Close();
            }
            return proizvod;
        }

        
        public static List<Proizvod> VratiProizvode()
        {
            List<Proizvod> lista = new List<Proizvod>();

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from Proizvod";
                SqlCommand komanda = new SqlCommand(upit, konekcija);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        Proizvod proizvod = new Proizvod();
                        proizvod.ProizvodID = Int32.Parse(citac["ProizvodID"].ToString());
                        proizvod.Naziv = citac["Naziv"].ToString();
                        proizvod.Kategorija = citac["Kategorija"].ToString();
                        lista.Add(proizvod);
                    }
                }
                konekcija.Close();
            }
            return lista;
        }

        //STAVKA KONFIGURACIJE ZURKE
        public static bool SacuvajStavkuKonfiguracije(StavkaKonfiguracijeZurke stavka)
        {
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                try
                {

                    SqlCommand komanda = new SqlCommand("dbo.DodajKonfiguraciju", konekcija);
                    komanda.CommandType = CommandType.StoredProcedure;

                    komanda.Parameters.AddWithValue("@ZurkaID", stavka.Zurka.ZurkaID);
                    komanda.Parameters.AddWithValue("@GostID", stavka.Gost.GostID);
                    komanda.Parameters.AddWithValue("@ProizvodID", stavka.Proizvod.ProizvodID);
                    komanda.Parameters.AddWithValue("@Kolicina", stavka.Kolicina);

                    komanda.ExecuteNonQuery();
                    konekcija.Close();
                    return true;
                }
                catch (Exception e)
                {
                    konekcija.Close();
                    return false;
                }
            }
        }

        public static List<StavkaKonfiguracijeZurke> PronadjiStavkeKonfiguracije(int ZurkaID, int GostID)
        {
            List<StavkaKonfiguracijeZurke> lista = new List<StavkaKonfiguracijeZurke>();

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from StavkaKonfiguracijeZurke where ZurkaID=@ZurkaID and GostID=@GostID";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@ZurkaID", ZurkaID);
                komanda.Parameters.AddWithValue("@GostID", GostID);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        StavkaKonfiguracijeZurke stavka = new StavkaKonfiguracijeZurke();
                        stavka.Zurka = Broker.PronadjiZurku(Int32.Parse(citac["ZurkaID"].ToString()));
                        stavka.Gost = Broker.UzmiGosta(Int32.Parse(citac["GostID"].ToString()));
                        stavka.Proizvod = Broker.UzmiProizvod(Int32.Parse(citac["ProizvodID"].ToString()));
                        stavka.Kolicina = float.Parse(citac["Kolicina"].ToString());

                        lista.Add(stavka);
                    }
                }
                konekcija.Close();
            }
            return lista;
        }

        public static StavkaKonfiguracijeZurke PronadjiStavkuKonfiguracije(int ZurkaID, int GostID, int ProizvodID)
        {
            StavkaKonfiguracijeZurke stavka = null;

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from StavkaKonfiguracijeZurke where ZurkaID=@ZurkaID and GostID=@GostID and ProizvodID=@ProizvodID";
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                komanda.Parameters.AddWithValue("@ZurkaID", ZurkaID);
                komanda.Parameters.AddWithValue("@GostID", GostID);
                komanda.Parameters.AddWithValue("@ProizvodID", ProizvodID);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        stavka = new StavkaKonfiguracijeZurke();
                        stavka.Zurka = Broker.PronadjiZurku(Int32.Parse(citac["ZurkaID"].ToString()));
                        stavka.Gost = Broker.UzmiGosta(Int32.Parse(citac["GostID"].ToString()));
                        stavka.Proizvod = Broker.UzmiProizvod(Int32.Parse(citac["ProizvodID"].ToString()));
                        stavka.Kolicina = float.Parse(citac["Kolicina"].ToString());
                        break;
                    }
                }
                konekcija.Close();
            }
            return stavka;
        }

        public static bool IzmeniStavkuKonfiguracije(StavkaKonfiguracijeZurke stavka, float Kolicina)
        {
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                try
                {

                    SqlCommand komanda = new SqlCommand("dbo.IzmeniKonfiguraciju", konekcija);
                    komanda.CommandType = CommandType.StoredProcedure;

                    komanda.Parameters.AddWithValue("@ZurkaID", stavka.Zurka.ZurkaID);
                    komanda.Parameters.AddWithValue("@GostID", stavka.Gost.GostID);
                    komanda.Parameters.AddWithValue("@ProizvodID", stavka.Proizvod.ProizvodID);
                    komanda.Parameters.AddWithValue("@Kolicina", Kolicina);

                    komanda.ExecuteNonQuery();
                    konekcija.Close();
                    return true;
                }
                catch (Exception e)
                {
                   
                    konekcija.Close();
                    return false;
                }
            }
        }

        public static bool ObrisiStavkuKonfiguracije(StavkaKonfiguracijeZurke stavka)
        {
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                try
                {

                    SqlCommand komanda = new SqlCommand("dbo.ObrisiStavku", konekcija);
                    komanda.CommandType = CommandType.StoredProcedure;

                    komanda.Parameters.AddWithValue("@ZurkaID", stavka.Zurka.ZurkaID);
                    komanda.Parameters.AddWithValue("@GostID", stavka.Gost.GostID);
                    komanda.Parameters.AddWithValue("@ProizvodID", stavka.Proizvod.ProizvodID);

                    komanda.ExecuteNonQuery();
                    konekcija.Close();
                    return true;
                }
                catch (Exception e)
                {
                    konekcija.Close();
                    return false;
                }
            }
        }

        public static bool ObrisiKonfiguraciju(int ZurkaID)
        {
            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                try
                {

                    SqlCommand komanda = new SqlCommand("dbo.ObrisiKonfiguraciju", konekcija);
                    komanda.CommandType = CommandType.StoredProcedure;

                    komanda.Parameters.AddWithValue("@ZurkaID", ZurkaID);

                    komanda.ExecuteNonQuery();
                    konekcija.Close();
                    return true;
                }
                catch (Exception e)
                {
                    konekcija.Close();
                    return false;
                }
            }
        }

        public static List<StavkaKonfiguracijeZurke> VratiKonfiguracije()
        {

            List<StavkaKonfiguracijeZurke> lista = new List<StavkaKonfiguracijeZurke>();

            using (SqlConnection konekcija = new SqlConnection("Data Source=lela-pc;Initial Catalog=OrganizacijaSlavlja;Integrated Security=True"))
            {
                konekcija.Open();
                string upit = "Select * from StavkaKonfiguracijeZurke";
                SqlCommand komanda = new SqlCommand(upit, konekcija);

                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                       StavkaKonfiguracijeZurke stavka = new StavkaKonfiguracijeZurke();
                       stavka.Gost = Broker.UzmiGosta(Int32.Parse(citac["GostID"].ToString()));
                       stavka.Proizvod = Broker.UzmiProizvod(Int32.Parse(citac["ProizvodID"].ToString()));
                       stavka.Kolicina = float.Parse(citac["Kolicina"].ToString());
                       stavka.Zurka = Broker.PronadjiZurku(Int32.Parse(citac["ZurkaID"].ToString()));
                       lista.Add(stavka);
                    }
                }
                konekcija.Close();
            }
            return lista;
        }
    }
}