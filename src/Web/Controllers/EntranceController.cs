using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Domain.Interfaces.Services;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace Web.Controllers
{
    [ExcludeFromCodeCoverage]
    public class EntranceController : BaseController<EntranceController>
    {
        private readonly IEntranceService _service;

        public EntranceController(IServiceProvider serviceProvider, ILogger<EntranceController> logger) : 
            base(serviceProvider, logger) => _service = GetService<IEntranceService>();

        [HttpGet("Entrance")]
        public ViewResult Index(
            string currentSort,
            string searchString, 
            int? page,
            int pageSize = 10
        )
        {
            currentSort = string.IsNullOrEmpty(currentSort) ? "" : currentSort;

            ViewBag.ValueSort = currentSort == "value" ? "value_desc" : "value";
            ViewBag.DescriptionSort = currentSort == "description" ? "description_desc" : "description";
            ViewBag.CategorySort = currentSort == "category" ? "category_desc" : "category";
            ViewBag.TypeSort = currentSort == "type" ? "type_desc" : "type";
            ViewBag.CreatedAtSort = currentSort == "createdAt" ? "" : "createdAt";

            if (searchString != null)
                page = 1;

            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            
            GetClaims();

            var entrances = _service.FindAllWithCategory(
                currentSort,
                searchString,
                UserId
            ).GetAwaiter().GetResult();

            var pageNumber = page ?? 1;

            return View(entrances?.ToPagedList(pageNumber, pageSize));
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
                
                return RedirectToAction("Index", "Entrance");
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
                
                return RedirectToAction("Index", "Entrance");
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
                
                return RedirectToAction("Index", "Entrance");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    }
}
