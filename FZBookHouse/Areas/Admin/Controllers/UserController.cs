using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FZBookHouse.Utilities;

namespace FZBookHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = SD.Role__Admin + "," + SD.Role__Emp)]
    public class UserController : Controller
    {
        //private readonly IUnitofWork _unitOfWork;
        //Direct Access by ApplicationDbContext;
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
     
        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _db.ApplicationUsers.Include(e => e.Company).ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach(var user in userList)
            {

                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

                if(user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = null
                    };

                }
            }
            return Json(new { data = userList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(e => e.Id == id);

            if(objFromDb == null)
            {
                return Json(new { seuccess=false, message = "Error While Locking/Unlocking"});
            }
            else if(objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked, we will unlock them
                objFromDb.LockoutEnd = DateTime.Now;

            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Opeartion Successfull" });
        }
        #endregion
    }
}
