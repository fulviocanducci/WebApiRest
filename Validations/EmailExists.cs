using System.ComponentModel.DataAnnotations;
using WebApiRest.Models;
using WebApiRest.Repositories;
using WebApiRest.ViewModels;

namespace WebApiRest.Validations
{
    public class EmailExists : ValidationAttribute
    {
        public EmailExists(string errorMessage) : base(errorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            UserRepositoryAbstract repository = 
                    (UserRepositoryAbstract)validationContext
                         .GetService(typeof(UserRepositoryAbstract));

            if (validationContext.ObjectInstance is IUserCreate modelc)
            {
                User item = repository.GetUserByEmailAsync(modelc.Email).Result;
                return item == null
                    ? ValidationResult.Success
                    : new ValidationResult(ErrorMessage);
            }
            if (validationContext.ObjectInstance is IUserEdit modele)
            {
                User item = repository.GetUserByEmailAsync(modele.Email).Result;
                if (item == null)
                {
                    return ValidationResult.Success;
                }
                return item.Id == modele.Id
                    ? ValidationResult.Success
                    : new ValidationResult(ErrorMessage);
            }
            return new ValidationResult(ErrorMessage);
        }                
    }
}
