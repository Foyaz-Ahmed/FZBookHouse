using FZBookHouse.Models;
using FZBookHouse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FZBookHouse.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType coverType);
    }
}
