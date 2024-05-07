﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> categories = _unitOfWork.Product.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product cat)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(cat);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if ((id ?? 0) == 0) return NotFound();

            Product? cat = _unitOfWork.Product.Get(c => c.Id == id);
            if (cat == null) return NotFound();

            return View(cat);
        }
        [HttpPost]
        public IActionResult Edit(Product cat)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(cat);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if ((id ?? 0) == 0) return NotFound();

            Product? cat = _unitOfWork.Product.Get(c => c.Id == id);
            if (cat == null) return NotFound();

            return View(cat);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? cat = _unitOfWork.Product.Get(c => c.Id == id);
            if (cat == null) return NotFound();

            _unitOfWork.Product.Remove(cat);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
