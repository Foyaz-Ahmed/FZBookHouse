using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FZBookHouse.Models
{
    public class Company
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        public string StreetAddress { set; get; }
        public string City { set; get; }
        public string State { set; get; }
        public string PostalCode { set; get; }
        public string PhoneNumber { set; get; }
        public bool IsAuthorizedCompany { set; get; }

    }
}
