using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SellerFormViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                var deps = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = obj.Seller, Departments = deps };
                return View(viewModel);
            }
            Seller objSeller = obj.Seller;
            await _sellerService.InsertAsync(objSeller);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
     
                if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" }); ;

                var obj = await _sellerService.FindByIdAsync(id.Value);

                if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;

                return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message }); ;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided"});;

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found"});;

            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided"});;

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found"});;

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult > Edit(int id, SellerFormViewModel objSellerModelView)
        {
            if (!ModelState.IsValid)
            {
                return View(objSellerModelView);
            }
            Seller obj = objSellerModelView.Seller;
            if (id != obj.Id)return BadRequest();

            try
            {
                await _sellerService.UpdateAsync(obj);
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
