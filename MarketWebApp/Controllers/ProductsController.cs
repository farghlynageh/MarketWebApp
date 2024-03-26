﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketWebApp.Data;
using MarketWebApp.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using MarketWebApp.Reprository.CategoryReprositry;
using MarketWebApp.Repository.ProductRepository;
using NuGet.Protocol.Core.Types;
using MarketWebApp.Repository.SupplierRepository;
using MarketWebApp.ViewModel.Product;

namespace MarketWebApp.Controllers
{
   // [Authorize(Roles = "Admin")]

    public class ProductsController : Controller
    {
        private readonly IProductRepository repository;
        private readonly ICategoryRepository repositorycaty;
        private readonly ISupplierRepository repositorySupplier;

        public ProductsController(IProductRepository repository, ICategoryRepository repositorycaty ,ISupplierRepository repositorySupplier)
        {
            this.repository = repository;
            this.repositorycaty = repositorycaty;
            this.repositorySupplier = repositorySupplier;

        }

        // GET: Products
        public IActionResult Index()
        {
            ViewBag.PageCount = (int)Math.Ceiling((decimal)repository.GetAll().Count() / 5m);
            return View(this.repository.GetAll());
        }

        public IActionResult GetProducts(int pageNumber, int pageSize = 5)
        {
            var products = repository.GetAll()
           .OrderBy(p => p.ID)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("_ProductTable", products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(repositorycaty.GetAll(), "ID", "Name");
            ViewData["SupplierId"] = new SelectList(repositorySupplier.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddProductViewModel addProductViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert the new product into the repository
                    repository.Insert(addProductViewModel);
                    repository.Save();

                    // Redirect to the index action after successful creation
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);

                    // Repopulate dropdown lists for categories and suppliers
                    PopulateDropdowns();
                    return View(addProductViewModel);
                }
            }
            else
            {
                // If the model state is not valid, return the view with the model to display validation errors
                return View(addProductViewModel);
            }
        }

        private void PopulateDropdowns()
        {
            // Populate dropdown lists for categories and suppliers
            ViewData["CategoryID"] = new SelectList(repositorycaty.GetAll(), "ID", "Name");
            ViewData["SupplierId"] = new SelectList(repositorySupplier.GetAll(), "ID", "Name");
        }


        // GET: Products/Edit/5
        [HttpGet]
       public IActionResult Edit(int Id)
        {
            var product = repository.GetProduct(Id);
            EditProductViewModel editProductViewModel = new EditProductViewModel();

            editProductViewModel.ID = product.ID;
            editProductViewModel.Name = product.Name;
            editProductViewModel.Img = product.Img;
            editProductViewModel.Price = product.Price;
            editProductViewModel.Discount = product.Discount;
            editProductViewModel.Stock = product.Stock;
            editProductViewModel.CategoryID = product.CategoryId;
            editProductViewModel.SupplierId = product.SupplierId;
            ViewData["CategoryID"] = new SelectList(repositorycaty.GetAll(), "ID", "Name", repositorycaty.GetCategory(editProductViewModel.CategoryID));
            ViewData["SupplierId"] = new SelectList(repositorySupplier.GetAll(), "ID", "Name", repositorySupplier.GetSupplier(editProductViewModel.SupplierId));

            return View(editProductViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProductViewModel editProductViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if a product with the same name already exists for the same supplier and category
                    if (!repository.IsProductUnique(editProductViewModel.ID, editProductViewModel.Name, editProductViewModel.SupplierId, editProductViewModel.CategoryID))
                    {
                        ModelState.AddModelError("Name", "Product name already exists for the same supplier and category.");
                        PopulateDropdowns(editProductViewModel); // Populate dropdowns again for the view
                        return View(editProductViewModel);
                    }

                    repository.Update(editProductViewModel);
                    repository.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    PopulateDropdowns(editProductViewModel); // Populate dropdowns again for the view
                    return View(editProductViewModel);
                }
            }
            else
            {
                PopulateDropdowns(editProductViewModel); // Populate dropdowns again for the view
                return View(editProductViewModel);
            }
        }

        private void PopulateDropdowns(EditProductViewModel editProductViewModel)
        {
            ViewData["CategoryID"] = new SelectList(repositorycaty.GetAll(), "ID", "Name", editProductViewModel.CategoryID);
            ViewData["SupplierId"] = new SelectList(repositorySupplier.GetAll(), "ID", "Name", editProductViewModel.SupplierId);
        }


        public IActionResult CheckProductExist(string Name)
        {
            if (repository.CheckProductExist(Name))
                return Json(true);
            else
                return Json(false);
        }
        public IActionResult CheckProductExistEdit(string Name, int Id)
        {
            if (repository.CheckProductExistEdit(Name, Id))
                return Json(true);
            else
                return Json(false);
        }

        // GET: Products/Delete/5
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            return View(repository.GetProduct(Id));

        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int Id)
        {

            if (ModelState.IsValid)
            {
                repository.Delete(Id);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View("Delete", repository.GetProduct(Id));
        }

        [HttpGet]
        public IActionResult GetProduct(int Id)
        {
            var allProducts = repository.GetAll();
            var currentProduct = allProducts.FirstOrDefault(p => p.ID == Id);


            var randomProducts = allProducts.Except(new List<Product> { currentProduct }).OrderBy(x => Guid.NewGuid()).Take(4);

            ViewData["Products"] = randomProducts.ToList();
            var product = repository.GetProduct(Id);
            return View(product);
        }

    }
}
