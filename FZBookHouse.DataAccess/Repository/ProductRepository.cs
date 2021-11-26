using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FZBookHouse.DataAccess.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var updateInfo = _db.Products.FirstOrDefault(e => e.Id == product.Id);

            if (updateInfo != null)
            {
                if(updateInfo.ImageUrl != null)
                {
                    updateInfo.ImageUrl = product.ImageUrl;
                }

                updateInfo.ISBN = product.ISBN;
                updateInfo.ListPrice = product.ListPrice;
                updateInfo.Price = product.Price;
                updateInfo.Price100 = product.Price100;
                updateInfo.Price50 = product.Price50;
                updateInfo.Title = product.Title;
                updateInfo.Author = product.Author;
                updateInfo.CategoryId = product.CategoryId;
                updateInfo.CoverTypeId = product.CoverTypeId;
                updateInfo.Description = product.Description;
                //_db.SaveChanges();
            }
            
        }
    }
}
