using FZBookHouse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FZBookHouse.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository:IRepository<Company>
    {
        void Update(Company comapnyy);
    }
}
