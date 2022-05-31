using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FZBookHouse.Models
{
    public class ShoppingCart
    {

        public ShoppingCart()
        {
            Count = 0;
        }
        [Key]
        public int Id { get; set; }
        public string  ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser  ApplicationUser { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Range(1,1000, ErrorMessage="Please Give the value Between 1 to 1000")]
        public int Count { set; get; }

        [NotMapped]
        public double Price { get; set; }
    }
}
