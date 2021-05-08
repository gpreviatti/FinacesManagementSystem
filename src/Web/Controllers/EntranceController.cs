using System;
using System.Collections.Generic;
using Domain.Dtos.Entrance;
using Domain.Dtos.EntranceTypeDto;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Entrance;
using Domain.Models;
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

        public IActionResult Index() => View();

        [HttpPost("Entrances/Datatables")]
        public IActionResult GetEntrancesDatatables(DatatablesModel<EntranceResultDto> datatablesModel)
        {
            datatablesModel.Draw = Request.Form["draw"].FirstOrDefault();
            datatablesModel.Start = Request.Form["start"].FirstOrDefault();
            datatablesModel.Length = Request.Form["length"].FirstOrDefault();
            datatablesModel.SortColumn = int.Parse(Request.Form["order[0][column]"].FirstOrDefault());
            datatablesModel.SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            datatablesModel.SearchValue = Request.Form["search[value]"].FirstOrDefault();

            datatablesModel = _service.FindAllAsyncWithCategoryDatatables(datatablesModel).Result;
            return Ok(datatablesModel);
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

        [HttpGet("Entrances/Edit/{Id}")]
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

        [HttpPost("Entrances/Edit/{Id}")]
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
