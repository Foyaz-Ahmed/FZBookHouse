using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using FZBookHouse.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FZBookHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize( Roles = SD.Role__Admin + "," + SD.Role__Emp)]
    public class CompanyController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        public CompanyController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();

            //insert company
            if (id == null)
            {
                return View(company);
            }

            //update company
            else
            {
                company = _unitOfWork.Company.Get(id.GetValueOrDefault());

                if (company == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(company);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();

                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Company.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var dataFromDb = _unitOfWork.Company.Get(id);

            if (dataFromDb != null)
            {
                _unitOfWork.Company.Remove(dataFromDb);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Deleted Successfully Done" });
            }
            return Json(new { success = false, message = "Error occured while Deleting" });
        }
        #endregion
    }
}
