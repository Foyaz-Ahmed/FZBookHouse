using FZBookHouse.DataAccess.Data;
using FZBookHouse.DataAccess.Repository;
using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using FZBookHouse.Models.ViewModels;
using FZBookHouse.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FZBookHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role__Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitofWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM() {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                { 
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            //insert product
            if (id == null)
            {
                return View(productVM);
            }
            
            //update product
            else
            {
                productVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());

                if(productVM.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(productVM);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                
                //For Image Update
                if(files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (productVM.Product.ImageUrl != null)
                    {
                        //this is for update and need to delete old image
                        var imagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);

                        }
                        productVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
                    }
                    else
                    {
                        //If Image Url Nothing Found for Insert
                        if(productVM.Product.Id == 0)
                        {
                            Product objFromDb = _unitOfWork.Product.Get(productVM.Product.Id);
                            productVM.Product.ImageUrl = objFromDb.ImageUrl;
                        }
                    }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();

                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                productVM.CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                if(productVM.Product.Id != 0)
                {
                    productVM.Product = _unitOfWork.Product.Get(productVM.Product.Id);
                }
            }
            return View(productVM);
        }
        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {

           var allObj = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
           // var allObj = _unitOfWork.Product.Include(mbox => mbox.Categroy, mbox => mbox.CoverType);
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var dataFromDb = _unitOfWork.Product.Get(id);

            if (dataFromDb == null)
            {
                _unitOfWork.Product.Remove(dataFromDb);
                return Json(new { success = false, message = "Error occured while Deleting" });
            }
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, dataFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitOfWork.Product.Remove(dataFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully Done"});
        }
        #endregion
    }
}
