using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.Entrance;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class EntranceController : BaseController<EntranceController>
    {
        private readonly IEntranceService _service;

        public EntranceController(IServiceProvider serviceProvider, ILogger<EntranceController> logger) : 
            base(serviceProvider, logger) => _service = GetService<IEntranceService>();

        public IActionResult Index() => View();

        [HttpPost("Entrances/Datatables")]
        public async Task<IActionResult> GetEntrancesDatatables(DatatablesModel<EntranceResultDto> datatablesModel)
        {
            try
            {
                GetClaims();
                datatablesModel.Draw = Request.Form["draw"].FirstOrDefault();
                datatablesModel.Start = Request.Form["start"].FirstOrDefault();
                datatablesModel.Length = Request.Form["length"].FirstOrDefault();
                datatablesModel.SortColumn = int.Parse(Request.Form["order[0][column]"].FirstOrDefault());
                datatablesModel.SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                datatablesModel.SearchValue = Request.Form["search[value]"].FirstOrDefault();

                datatablesModel = await _service.FindAllAsyncWithCategoryDatatables(datatablesModel, UserId);
                return Ok(datatablesModel);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                GetClaims();
                var result = await _service.SetupEntranceCreateViewModel(UserId);
                return View(result);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EntranceCreateViewModel entraceCreateViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.CreateAsync(entraceCreateViewModel.Entrance);
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
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                GetClaims();
                var result = await _service.SetupEntranceUpdateViewModel(UserId, id);
                return View(result);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost("Entrances/Edit/{Id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EntranceUpdateViewModel entraceUpdateView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.UpdateAsync(entraceUpdateView.Entrance);
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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.DeleteAsync(id);
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
