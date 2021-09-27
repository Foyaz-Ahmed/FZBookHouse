using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FZBookHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        public CategoryController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();

            //insert category
            if (id == null)
            {
                return View(category);
            }
            
            //update category
            else
            {
                category = _unitOfWork.Category.Get(id.GetValueOrDefault());

                if(category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(category);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();

                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var dataFromDb = _unitOfWork.Category.Get(id);

            if (dataFromDb != null)
            {
                _unitOfWork.Category.Remove(dataFromDb);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Deleted Successfully Done"});
            }
            return Json(new { success = false, message = "Error occured while Deleting"});
        }
        #endregion
    }
}
