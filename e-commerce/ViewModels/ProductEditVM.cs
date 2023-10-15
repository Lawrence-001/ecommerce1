using e_commerce.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace e_commerce.ViewModels
{
    public class ProductEditVM : ProductCreateVM
    {
        public int ProductId { get; set; }
        public string ExistingPhoto { get; set; }
    }
}
