using System.ComponentModel.DataAnnotations;
using WebApiRest.Validations;

namespace WebApiRest.ViewModels
{

    public class UserEdit: IUserEdit
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [EmailExists("Duplicate E-mail")]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}