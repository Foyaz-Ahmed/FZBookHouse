using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository.IRepository;
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
            SP_Call = new SP_Call(_db);
        }

        private object CategoryRepository(ApplicationDbContext db)
        {
            throw new NotImplementedException();
        }

        public ICategoryRepository Category { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        public ISP_Call ISP_Call => throw new NotImplementedException();

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
