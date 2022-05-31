using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using FZBookHouse.DataAccess.Repository;
using FZBookHouse.Models.ViewModels;
using FZBookHouse.Utilities;
//using FZBookHouse.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace FZBookHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = SD.Role__Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        public CoverTypeController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();

            //insert coverType
            if (id == null)
            {
                return View(coverType);
            }
            
            //update coverType
            else
            {
                //coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());

                //use Stroe Procedure
                var parameter = new DynamicParameters();
                parameter.Add("@Id", id);
                coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);

                if (coverType == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(coverType);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", coverType.Name);

                if (coverType.Id == 0)
                {
                    // _unitOfWork.CoverType.Add(coverType);

                    //use Stroe Procedure
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, parameter);
                }
                else
                {
                    // _unitOfWork.CoverType.Update(coverType);

                    //use Stroe Procedure
                    parameter.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, parameter);
                }
                _unitOfWork.Save();

                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }
        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            //use Stroe Procedure
            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_Get_All, null);
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            //var dataFromDb = _unitOfWork.CoverType.Get(id);

            //use Stroe Procedure
            var dataFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);
      
            if (dataFromDb != null)
            {
                //  _unitOfWork.CoverType.Remove(dataFromDb);

                //use Stroe Procedure
                _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, parameter);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Deleted Successfully Done"});
            }
            return Json(new { success = false, message = "Error occured while Deleting"});
        }
        #endregion
    }
}
