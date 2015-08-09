using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnotherShopping.Models
{
    public class CartModels
    {
        string cartId;
        [Key]
        public string CartId
        {
            get { return cartId; }
            set { cartId = value; }
        }
        string productId;
        [Required]
        public string ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        string userId;
        [Required]
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
    }
    public class CartGuiModel
    {
        CartModels cart;
        public CartModels Cart
        {
            get { return cart; }
            set { cart = value; }
        }
        ProductModels product;
        public ProductModels Product
        {
            get { return product; }
            set { product = value; }
        }
        UserProfile profile;
        public UserProfile Profile
        {
            get { return profile; }
            set { profile = value; }
        }
        int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
}