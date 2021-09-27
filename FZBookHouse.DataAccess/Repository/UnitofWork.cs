using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FZBookHouse.DataAccess.Repository
{
    public class UnitofWork: IUnitofWork
    {
        private readonly ApplicationDbContext _db;

        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
