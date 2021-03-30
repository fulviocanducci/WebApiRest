using System.ComponentModel.DataAnnotations;
using WebApiRest.Validations;

namespace WebApiRest.ViewModels
{
    public class UserCreate: IUserCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [EmailExists("Duplicate E-mail")]
        public string Email { get; set; }
        
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}