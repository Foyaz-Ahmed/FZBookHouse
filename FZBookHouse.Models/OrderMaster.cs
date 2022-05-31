using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FZBookHouse.Models
{
   public class OrderMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { set; get; }
        [Required]
        public DateTime ShippingDate { set; get; }
        public DateTime PaymentDate { set; get; }
        public DateTime PaymentDueDate { set; get; }
        public Double OrderTotal  { get; set; }
        public string  TrackingNumber { get; set; }
        public string  Carrier { get; set; }
        public string  OrderStatus { get; set; }
        public string  PaymentStatus { get; set; }
        public string  TransactionId { get; set; }
        public string  PhoneNumber { get; set; }
        public string  StreetAddress { get; set; }
        public string  City { get; set; }
        public string  State { get; set; }
        public string  Name { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
