using System.ComponentModel.DataAnnotations;

namespace e_commerce.ViewModels
{
    public class RegistrationVM
    {
        [Required(ErrorMessage ="First name is required")]
        [Display (Name ="First Name")]
        public string FirstName { get; set; }
        [Required (ErrorMessage ="Last name is required")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }
        [Required (ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required (ErrorMessage = "Confirm password is required")]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
