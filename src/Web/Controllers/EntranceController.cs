using System;
using System.Collections.Generic;
using Domain.Dtos.Entrance;
using Domain.Dtos.EntranceTypeDto;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Entrance;
using System.Linq;

namespace Web.Controllers
{
    public class EntranceController : Controller
    {
        private readonly IEntranceService _service;
        private readonly IWalletService _walletService;
        private readonly ICategoryService _categoryService;
        private readonly IEnumerable<EntranceTypeResultDto> _entraceTypesResultDto;

        public EntranceController(IEntranceService service, IWalletService walletService, ICategoryService categoryService)
        {
            _service = service;
            _walletService = walletService;
            _categoryService = categoryService;
            _entraceTypesResultDto = new List<EntranceTypeResultDto>()
            {
                new EntranceTypeResultDto() { Value = 1, Name = "Income"},
                new EntranceTypeResultDto() { Value = 2, Name = "Expanse"},
                new EntranceTypeResultDto() { Value = 3, Name = "Transferance"},
            };
        }

        public IActionResult Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber
        )
        {
            //ViewData["Description"] = sortOrder == "Description" ? "Description" : "";
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            var entraces = _service.FindAllAsyncWithCategory(sortOrder, currentFilter, searchString, pageNumber).Result;
            return View(entraces);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var entraceCreateViewModel = new EntranceCreateViewModel();
            entraceCreateViewModel.Entrance = new EntranceCreateDto();
            entraceCreateViewModel.Wallets = _walletService.FindAsyncWalletsUser().Result;
            entraceCreateViewModel.Categories = _categoryService.FindAsyncAllCommonAndUserCategories().Result;
            entraceCreateViewModel.EntranceTypes = _entraceTypesResultDto;
            return View(entraceCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EntranceCreateViewModel entraceCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.CreateAsync(entraceCreateViewModel.Entrance).Result;
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Edit/{Id}")]
        public IActionResult Edit(Guid id)
        {
            var entraceUpdateViewModel = new EntranceUpdateViewModel();
            entraceUpdateViewModel.Entrance = _service.FindByIdUpdateAsync(id).Result;
            entraceUpdateViewModel.Wallets = _walletService.FindAsyncWalletsUser().Result;
            entraceUpdateViewModel.Categories = _categoryService.FindAsyncAllCommonAndUserCategories().Result;
            entraceUpdateViewModel.EntranceTypes = new List<EntranceTypeResultDto>()
            {
                new EntranceTypeResultDto() { Value = 1, Name = "Income"},
                new EntranceTypeResultDto() { Value = 2, Name = "Expanse"},
                new EntranceTypeResultDto() { Value = 3, Name = "Transferance"},
            };
            return View(entraceUpdateViewModel);
        }

        [HttpPost("Edit/{Id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, EntranceUpdateViewModel entraceUpdateView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.UpdateAsync(entraceUpdateView.Entrance).Result;
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet("Entrance/Delete/{Id}")]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.DeleteAsync(id).Result;
            if (result.Equals(null))
            {
                return BadRequest(ModelState);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
