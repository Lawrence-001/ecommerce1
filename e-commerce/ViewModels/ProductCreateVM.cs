using e_commerce.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.ViewModels
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name ="Category")]
        public ProductCategory  ProductCategory { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
       // [Column (TypeName ="decimal(18, 2")]
        public double Cost { get; set; }
    }
}
