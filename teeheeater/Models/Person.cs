using System.ComponentModel.DataAnnotations;

namespace teeheeater.Models
{
   public class Person
    
    {
        [Required(ErrorMessage = "Alstublieft uw voornaam invullen")] 
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Alstublieft uw achternaam invullen")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Alstublieft uw e-mail invullen")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}