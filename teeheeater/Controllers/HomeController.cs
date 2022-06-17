using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using teeheeater.Database;
using teeheeater.Models;
using System.Security.Cryptography;
using System.Text;

namespace teeheeater.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // private int lastName;

        public object ViewDate { get; private set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //ViewData["user"] = HttpContext.Session.GetString("User");
            return View();
        }

        [Route("nieuws")]
        public IActionResult Nieuws()
        {
            return View();
        }

        [Route("voorstellingen")]
        public IActionResult Voorstellingen()
        {
            var products = GetAllVoorstellingen();

            // de lijst met producten in de html stoppen
            return View(products);
        }
     

        [Route("voorstellingen/{id}")]
        public IActionResult VoorstellingenDetails(int id)
        {
            var voorstelling = GetAgendaVoorstelling(id);

            return View(voorstelling);
        }


        [Route("voorstellingen/{id}/kaartjes")]
        public IActionResult VoorstellingenKaartjes(int id)
        {
            var voorstelling = GetAgendaVoorstelling(id);

            return View(voorstelling);
        }

        [Route("agenda")]
        public IActionResult Agenda()
        {
            var products = GetAllAgendaVoorstellingen();

            return View(products);
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("contact")]
        public IActionResult Contact(Person person)
        {
            // hebben we alles goed ingevuld? dan sturen we de gebruiker door naar de succes pagina
            if (ModelState.IsValid)
            {
                //persoon opslaan in de database
                DatabaseConnector.SavePerson(person);

                return Redirect("/succes");
            }

            // niet goed? dan sturen we de gegevens door naar de view, zodat we de fouten kunnen tonen
            return View(person);
        }

        [Route("succes")]
        public IActionResult Succes()
        {
            return View();
        }

        [Route("404")]
        public IActionResult Error()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        public IActionResult Login(string username, string wachtwoord)
        {

            if (wachtwoord == "geheim")
            {
                HttpContext.Session.SetString("User", username);
                //return Redirect("/");
                // hash voor "wachtwoord"
               // string hash = "dc00c903852bb19eb250aeba05e534a6d211629d77d055033806b783bae09937";

                // is er een wachtwoord ingevoerd?
                //if (!string.IsNullOrWhiteSpace(wachtwoord))
                //{

                //    //Er is iets ingevoerd, nu kunnen we het wachtwoord hashen en vergelijken met de hash "uit de database"
                //    //string hashVanIngevoerdWachtwoord = ComputeSha256Hash(wachtwoord);
                //    if (hashVanIngevoerdWachtwoord == hash)
                //    {
                //        HttpContext.Session.SetString("User", username);
                //        return Redirect("/");
                //    }
                //}

                return View();

            }

            return View();
        }

        public List<Voorstellingen> GetAllAgendaVoorstellingen()
        {
            // alle producten ophalen uit de database
            var rows = DatabaseConnector.GetRows("select * from voorstellingagenda INNER JOIN voorstellingen ON voorstellingagenda.voorstelling_id = voorstellingen.id");

            // lijst maken om alle producten in te stoppen
            List<Voorstellingen> voorstellingen = new List<Voorstellingen>();

            foreach (var row in rows)
            {
                Voorstellingen p = new Voorstellingen();
                p.Naam = row["Naam"].ToString();
                p.Beschrijving = row["Beschrijving"].ToString();
                p.Foto = row["Foto"].ToString();
                p.Id = Convert.ToInt32(row["id"]);
                p.Datum = row["datum"].ToString();
                p.Tijd = row["tijd"].ToString();
                p.Ticketvoorraad = Convert.ToInt32(row["ticketvoorraad"]);

                // en dat product voegen we toe aan de lijst met producten
                voorstellingen.Add(p);
            }

            return voorstellingen;
        }

        public Voorstellingen GetAgendaVoorstelling(int id)
                {
                    // alle producten ophalen uit de database
                    var rows = DatabaseConnector.GetRows($"select * from voorstellingagenda INNER JOIN voorstellingen ON voorstellingagenda.voorstelling_id = voorstellingen.id WHERE voorstellingen.id = {id}");

                    // lijst maken om alle producten in te stoppen
                    List<Voorstellingen> products = new List<Voorstellingen>();

                    foreach (var row in rows)
                    {
                        // Voor elke rij maken we nu een product
                        Voorstellingen p = new Voorstellingen();
                        p.Naam = row["Naam"].ToString();
                        p.Beschrijving = row["Beschrijving"].ToString();
                        p.Foto = row["Foto"].ToString();
                        p.Id = Convert.ToInt32(row["id"]);
                        p.Datum = row["datum"].ToString();
                        p.Tijd = row["tijd"].ToString();
                        p.Ticketvoorraad = Convert.ToInt32(row["ticketvoorraad"]);


                // en dat product voegen we toe aan de lijst met producten
                products.Add(p);
                    }

                    return products[0];
                }

        public List<Voorstellingen> GetAllVoorstellingen()
        {
            // alle producten ophalen uit de database
            var rows = DatabaseConnector.GetRows("select * from voorstellingen");

            // lijst maken om alle producten in te stoppen
            List<Voorstellingen> voorstellingen = new List<Voorstellingen>();

            foreach (var row in rows)
            {
                Voorstellingen p = new Voorstellingen();
                p.Naam = row["Naam"].ToString();
                p.Beschrijving = row["Beschrijving"].ToString();
                p.Foto = row["Foto"].ToString();
                p.Id = Convert.ToInt32(row["id"]);

                // en dat product voegen we toe aan de lijst met producten
                voorstellingen.Add(p);
            }

            return voorstellingen;
        }

        //public Voorstellingen GetVoorstelling(int id)
        //{
        //    // alle producten ophalen uit de database
        //    var rows = DatabaseConnector.GetRows($"select * from voorstellingen WHERE id = {id}");

        //    // lijst maken om alle producten in te stoppen
        //    List<Voorstellingen> products = new List<Voorstellingen>();

        //    foreach (var row in rows)
        //    {
        //        // Voor elke rij maken we nu een product
        //        Voorstellingen p = new Voorstellingen();
        //        p.Naam = row["Naam"].ToString();
        //        p.Beschrijving = row["Beschrijving"].ToString();
        //        p.Foto = row["Foto"].ToString();
        //        p.Id = Convert.ToInt32(row["id"]);

        //        // en dat product voegen we toe aan de lijst met producten
        //        products.Add(p);
        //    }

        //    return products[0];
        //}
    }
}
