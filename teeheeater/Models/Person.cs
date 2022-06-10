using System.ComponentModel.DataAnnotations;

namespace teeheeater.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Gelieve uw voornaam in te vullen")]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Achternaam is een verplicht veld")]
        [Display(Name = "Achternaam")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Emailadres is verplicht")]
        [EmailAddress(ErrorMessage = "Geen geldig email adres")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Wachtwoord")]
        public string Wachtwoord { get; set; }

        [Display(Name = "Telefoon")]
        public string Telefoon { get; set; }

        [Display(Name = "Adres")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Bericht is verplicht")]

        [Display(Name = "Bericht")]
        public string Bericht { get; set; }


    }


}