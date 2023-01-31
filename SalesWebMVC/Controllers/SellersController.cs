using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;

        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SellerFormViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                var deps = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = obj.Seller, Departments = deps };
                return View(viewModel);
            }
            Seller objSeller = obj.Seller;
            _sellerService.Insert(objSeller);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided"});;

            var obj = _sellerService.FindById(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found"});;

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided"});;

            var obj = _sellerService.FindById(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found"});;

            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided"});;

            var obj = _sellerService.FindById(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found"});;

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SellerFormViewModel objSellerModelView)
        {
            if (!ModelState.IsValid)
            {
                return View(objSellerModelView);
            }
            Seller obj = objSellerModelView.Seller;
            if (id != obj.Id)return BadRequest();

            try
            {
                _sellerService.Update(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});;
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message }); ;
            }

        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
