using System;
using System.Collections.Generic;
using System.Text;

namespace FZBookHouse.DataAccess.Repository.IRepository
{
    public interface IUnitofWork: IDisposable
    {
        ICategoryRepository Category { get; }
        ISP_Call ISP_Call { get; }
    }
}
