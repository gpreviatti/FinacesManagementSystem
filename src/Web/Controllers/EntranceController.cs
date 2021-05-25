using System;
using System.Collections.Generic;
using Domain.Dtos.Entrance;
using Domain.Dtos.EntranceTypeDto;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Entrance;
using Domain.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class EntranceController : BaseController<EntranceController>
    {
        private readonly IEntranceService _service;
        private readonly IWalletService _walletService;
        private readonly ICategoryService _categoryService;
        private readonly IEnumerable<EntranceTypeResultDto> _entraceTypesResultDto;

        public EntranceController(
            IEntranceService service, 
            IWalletService walletService, 
            ICategoryService categoryService, 
            ILogger<EntranceController> logger
        ) : base(logger)
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
            try
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
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var entraceCreateViewModel = new EntranceCreateViewModel();
                entraceCreateViewModel.Entrance = new EntranceCreateDto();
                entraceCreateViewModel.Wallets = _walletService.FindAsyncWalletsUser().Result;
                entraceCreateViewModel.Categories = _categoryService.FindAsyncAllCommonAndUserCategories().Result;
                entraceCreateViewModel.EntranceTypes = _entraceTypesResultDto;
                return View(entraceCreateViewModel);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EntranceCreateViewModel entraceCreateViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _service.CreateAsync(entraceCreateViewModel.Entrance).Result;
                if (result == null)
                    return BadRequest(ModelState);

                LoggingWarning($"Entrance {result.Id} created with success");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpGet("Entrances/Edit/{Id}")]
        public IActionResult Edit(Guid id)
        {
            try
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
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost("Entrances/Edit/{Id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, EntranceUpdateViewModel entraceUpdateView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _service.UpdateAsync(entraceUpdateView.Entrance).Result;
                if (result == null)
                    return BadRequest(ModelState);

                LoggingWarning($"Entrance {result.Id} updated with success");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }


        [HttpGet("Entrance/Delete/{Id}")]
        public IActionResult Delete(Guid id)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _service.DeleteAsync(id).Result;
                if (result.Equals(null))
                    return BadRequest(ModelState);

                LoggingWarning($"Entrance {id} deleted with success");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    }
}
