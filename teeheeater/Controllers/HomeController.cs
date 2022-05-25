using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadDatabase.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using teeheeater.Database;
using teeheeater.Models;

namespace teeheeater.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private int lastName;

        public object ViewDate { get; private set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // alle producten ophalen
            var products = GetAllProducts();

            return View(products);
        }

        [Route("agenda")]
        public IActionResult Agenda()
        {
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
            return View();
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

        [Route("product/{id}")]
        public IActionResult ProductDetails(int id)
        {
            var product = GetProduct(id);
            return View(product);
        }
        public IActionResult Agenda()
        {
            return View();
        }

        public Product GetProduct (int id)
        {
            // alle producten ophalen uit de database
            var rows = DatabaseConnector.GetRows("Select * from product");

            // lijst maken om alle producten in te stoppen                                                                                                                                              qqq

            List<Product> products = new List<Product>();

            foreach (var row in rows)
            {
                // Voor elke rij maken we nu een product
                Product p = new Product();
                p.Naam = row["naam"].ToString();
                p.Prijs = row["prijs"].ToString();
                p.Beschikbaarheid = Convert.ToInt32(row["beschikbaarheid"]);
                p.Id = Convert.ToInt32(row["Id"]);

                // en dat product voegen we toe aan de lijst met producten
                products.Add(p);
            }
            return products;
        }
        public List<Product> GetAllProducts()
        {
            // alle producten ophalen uit de database
            var rows = DatabaseConnector.GetRows("Select * from product");

            // lijst maken om alle producten in te stoppen
            List<Product> products = new List<Product>();

            foreach (var row in rows)
            {
                // Voor elke rij maken we nu een product
                Product p = new Product();
                p.Naam = row["naam"].ToString();
                p.Prijs = row["prijs"].ToString();
                p.Beschikbaarheid = Convert.ToInt32(row["beschikbaarheid"]);
                p.Id = Convert.ToInt32(row["Id"]);

                // en dat product voegen we toe aan de lijst met producten
                products.Add(p);
            }
            return products;
        }
    }
}


