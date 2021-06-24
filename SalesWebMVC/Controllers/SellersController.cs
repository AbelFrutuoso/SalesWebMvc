using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;
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
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                SellerFormViewModel sellerFormViewModel = await GetSellerFormViewModelAsync(seller);
                return View(sellerFormViewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var seller = await GetSellerAsync(id);

            if (seller == null) return RedirectToAction(nameof(Error),new { message = "Invalid Id!" });

            return View(seller);
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
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        public async Task<IActionResult> Details (int? id)
        {
            var seller = await GetSellerAsync(id);

            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Invalid Id!" });

            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var seller = await GetSellerAsync(id);

            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Invalid Id!" });

            SellerFormViewModel sellerFormViewModel = await GetSellerFormViewModelAsync(seller);

            return View(sellerFormViewModel);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                SellerFormViewModel sellerFormViewModel = await GetSellerFormViewModelAsync(seller);
                return View(sellerFormViewModel);
            }
            

            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Invalid Id!" });

            try
            {
                await _sellerService.UpDateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
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

        private async Task<Seller> GetSellerAsync(int? Id)
        {
            if (Id == null) return null;

            var seller = await _sellerService.FindByIdAsync(Id.Value);

            if (seller == null) return null;

            return seller;
        }

        private async Task<SellerFormViewModel> GetSellerFormViewModelAsync(Seller seller)
        {
            List<Department> departments = await _departmentService.FindAllAsync();
            return new SellerFormViewModel { Seller = seller, Departments = departments };
        }

    }
}
