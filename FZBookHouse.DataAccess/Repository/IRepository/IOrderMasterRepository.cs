using FZBookHouse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FZBookHouse.DataAccess.Repository.IRepository
{
    public interface IOrderMasterRepository:IRepository<OrderMaster>
    {
        void Update(OrderMaster orderMaster);
    }
}
