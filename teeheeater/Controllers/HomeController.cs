using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            return View();
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
        public IActionResult Error();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
