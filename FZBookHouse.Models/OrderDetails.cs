using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FZBookHouse.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { set; get; }
        [ForeignKey("OrderId")]
        public OrderMaster OrderMaster { get; set; }

        public int ProductId { set; get; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int Count { get; set; }
        public double Price { set; get; }
    }
}
