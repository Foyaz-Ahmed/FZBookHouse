﻿using FZBookHouse.Models;
using FZBookHouse.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FZBookHouse.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
      
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; } 
        public DbSet<Product> Products { get; set; } 
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } 
        public DbSet<Company> Companies { get; set; } 
        public DbSet<ShoppingCart> ShoppingCart { get; set; } 
        public DbSet<OrderMaster> OrderMaster { get; set; } 
        public DbSet<OrderDetails> OrderDetails { get; set; } 
    }
}
