using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FZBookHouse.DataAccess.Repository
{
    public class OrderMasterRepository: Repository<OrderMaster>, IOrderMasterRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderMasterRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(OrderMaster obj)
        {
            _db.Update(obj);
        }
    }
}
