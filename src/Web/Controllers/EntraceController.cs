using System;
using System.Collections.Generic;
using Domain.Dtos.Entrace;
using Domain.Dtos.EntraceTypeDto;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Entrace;

namespace Web.Controllers
{
    public class EntraceController : Controller
    {
        private readonly IEntraceService _service;
        private readonly IWalletService _walletService;
        private readonly ICategoryService _categoryService;
        private readonly IEnumerable<EntraceTypeResultDto> _entraceTypesResultDto;

        public EntraceController(IEntraceService service, IWalletService walletService, ICategoryService categoryService)
        {
            _service = service;
            _walletService = walletService;
            _categoryService = categoryService;
            _entraceTypesResultDto = new List<EntraceTypeResultDto>()
            {
                new EntraceTypeResultDto() { Value = 1, Name = "Income"},
                new EntraceTypeResultDto() { Value = 2, Name = "Expanse"},
                new EntraceTypeResultDto() { Value = 3, Name = "Transferance"},
            };
        }

        public IActionResult Index()
        {
            var entraces = _service.FindAllAsync().Result;
            return View(entraces);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var entraceCreateView = new EntraceCreateViewModel();
            entraceCreateView.Entrace = new EntraceCreateDto();
            entraceCreateView.Wallets = _walletService.FindAllAsync().Result;
            entraceCreateView.Categories = _categoryService.FindAllAsync().Result;
            entraceCreateView.EntraceTypes = _entraceTypesResultDto;
            return View(entraceCreateView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EntraceCreateViewModel entraceCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.CreateAsync(entraceCreateViewModel.Entrace).Result;
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Edit/{Id}")]
        public IActionResult Edit(Guid id)
        {
            var entraceUpdateViewModel = new EntraceUpdateViewModel();
            entraceUpdateViewModel.Entrace = _service.FindByIdUpdateAsync(id).Result;
            entraceUpdateViewModel.Wallets = _walletService.FindAllAsync().Result;
            entraceUpdateViewModel.Categories = _categoryService.FindAllAsync().Result;
            entraceUpdateViewModel.EntraceTypes = new List<EntraceTypeResultDto>()
            {
                new EntraceTypeResultDto() { Value = 1, Name = "Income"},
                new EntraceTypeResultDto() { Value = 2, Name = "Expanse"},
                new EntraceTypeResultDto() { Value = 3, Name = "Transferance"},
            };
            return View(entraceUpdateViewModel);
        }

        [HttpPost("Edit/{Id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, EntraceUpdateViewModel entraceUpdateView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.UpdateAsync(entraceUpdateView.Entrace).Result;
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet("Entrace/Delete/{Id}")]
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
