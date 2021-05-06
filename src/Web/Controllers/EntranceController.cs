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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("entrances/GetEntrancesDatatables")]
        public IActionResult GetEntrancesDatatables()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var entrancesData = _service.FindAllAsyncWithCategory().Result;
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                entrancesData = entrancesData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                entrancesData = entrancesData.Where(m => m.Description.Contains(searchValue) || m.Observation.Contains(searchValue) || m.Category.Name.Contains(searchValue)).ToList();
            }
            recordsTotal = entrancesData.Count();
            var data = entrancesData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
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
