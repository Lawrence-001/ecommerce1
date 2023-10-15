using e_commerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.ViewModels
{
    public class CartVM
    {
        public Cart Cart { get; set; }
        public decimal CartTotal { get; set; }

    }
}
