using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models
{
    public class Payment
    {
        [Display(Name ="Phone Number")]
        public string? phoneNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
