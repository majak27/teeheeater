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
            var voorstelling = GetVoorstelling(id);

            return View(voorstelling);
        }

        [Route("agenda")]
        public IActionResult Agenda()
        {
            return View();
        }

        [HttpPost]
        [Route("contact")]
        public IActionResult Contact(Person person)
        {
            // hebben we alles goed ingevuld? dan sturen we de gebruiker door naar de succes pagina
            if (ModelState.IsValid)
                return Redirect("/succes");

            // niet goed? dan sturen we de gegevens door naar de view, zodat we de fouten kunnen tonen
            return View(person);
        }

        [Route("succes")]
        public IActionResult Succes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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

        public Voorstellingen GetVoorstelling(int id)
        {
            // alle producten ophalen uit de database
            var rows = DatabaseConnector.GetRows($"select * from voorstellingen where id = {id}");

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

                // en dat product voegen we toe aan de lijst met producten
                products.Add(p);
            }

            return products[0];
        }

        //public IActionResult Login(string username, string password)
        //{
        //    if (password == "geheim")
        //    {
        //        HttpContext.Session.SetString("User", username);
        //        return redirect("/");
        //    }
        //    return View();
        //}

    }
}
