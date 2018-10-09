using System.ComponentModel.DataAnnotations;

namespace SitemaLanche.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Usuario")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
