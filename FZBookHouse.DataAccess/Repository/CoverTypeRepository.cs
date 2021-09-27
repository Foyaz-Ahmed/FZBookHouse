using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using FZBookHouse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FZBookHouse.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(CoverType coverType)
        {
            var updateInfo = _db.CoverTypes.FirstOrDefault(e => e.Id == coverType.Id);

            if (updateInfo != null)
            {
                updateInfo.Name = coverType.Name;
                //_db.SaveChanges();
            }
            
        }
    }
}
