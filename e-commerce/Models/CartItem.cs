namespace e_commerce.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string CartId { get; set; }

    }
}
