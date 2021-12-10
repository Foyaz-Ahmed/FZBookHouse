using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FZBookHouse.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string City { set; get; }
        public string PostalCode { set; get; }
        public int? CompanyId { set; get; }
        [ForeignKey("CompanyId")]
        public Company Company { set; get; }
        [NotMapped]
        public string Role { set; get; }

    }
}
