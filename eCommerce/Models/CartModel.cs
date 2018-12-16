
using eCommerce.EntityFramework;
using System.Collections.Generic;

namespace eCommerce.Models
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }

        public int TotalQuantity { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class UpdateCartRequestModel
    {
        public long Id { get; set; }

        public int Quantity { get; set; }
    }

    public class UserCartModel
    {
        public List<CartItem> CartItems { get; set; }

        public int TotalQuantity { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class CartResponseModel<T> where T : class
    {
        public bool Result { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }

    public class CartResponseModel
    {
        public bool Result { get; set; }

        public string Message { get; set; }
    }

    public class UpdateCartModel
    {
        public long Id { get; set; }

        public string Image { get; set; }

        public string Url { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        public int TotalQuantity { get; set; }

        public string TotalAmount { get; set; }

        public string Name { get; set; }
    }
}