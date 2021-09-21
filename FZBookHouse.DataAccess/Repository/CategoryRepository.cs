using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FZBookHouse.DataAccess.Repository
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var updateInfo = _db.Categories.FirstOrDefault(e => e.Id == category.Id);

            if (updateInfo != null)
            {
                updateInfo.Name = category.Name;
                _db.SaveChanges();
            }
            
        }
    }
}
