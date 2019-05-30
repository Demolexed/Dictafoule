using System.ComponentModel.DataAnnotations;

namespace DictaFoule.Web.Models.Authentication
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Identifiant")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }
    }
}