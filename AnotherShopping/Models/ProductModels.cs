using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnotherShopping.Models
{
    public class ProductModels
    {
        string productId;
        [Key]
        public string ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        string productName;
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        string productImage;
        [Display(Name = "Product Image")]
        public string ProductImage
        {
            get { return productImage; }
            set { productImage = value; }
        }
        private float productPrice;
        [Required]
        [Display(Name = "Product Price")]
        public float ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }
        private int productAvailable;
        [Required]
        [Display(Name = "Product Available")]
        public int ProductAvailable
        {
            get { return productAvailable; }
            set { productAvailable = value; }
        }
        string productDetails;
        [Display(Name = "Product Details")]
        public string ProductDetails
        {
            get { return productDetails; }
            set { productDetails = value; }
        }
        string createdTime;
        [DataType(DataType.DateTime)]
        [Display(Name = "Product Created Time")]
        public string CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }
    }
}