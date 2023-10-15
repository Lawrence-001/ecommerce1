using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace e_commerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Category")]
        public ProductCategory? ProductCategory { get; set; }
        public string ImgUrl { get; set; }
        [Required]
       // [Column(TypeName ="decimal(18,2)")]
        public double Cost { get; set; }

    }
}
