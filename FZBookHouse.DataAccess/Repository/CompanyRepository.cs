using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FZBookHouse.DataAccess.Repository
{
    public class CompanyRepository: Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Company comapany)
        {
            _db.Update(comapany);   
        }
    }
}
