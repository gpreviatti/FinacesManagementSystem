using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.Category;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class CategoryController : BaseController<CategoryController>
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service, ILogger<CategoryController> logger) : base(logger) =>  _service = service;

        public IActionResult Index() => View();

        [HttpPost("Categories/Datatables")]
        public async Task<IActionResult> GetDatatables(DatatablesModel<CategoryResultDto> datatablesModel)
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

                datatablesModel = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModel, UserId);
                return Ok(datatablesModel);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        public async Task <ActionResult> Create()
        {
            try
            {
                var categoryCreateViewModel = new CategoryCreateViewModel();
                categoryCreateViewModel.Category = new CategoryCreateDto();
                GetClaims();
                categoryCreateViewModel.Categories = await _service.FindAsyncAllCommonAndUserCategories(UserId);
                return View(categoryCreateViewModel);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryCreateViewModel categoryCreateViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                GetClaims();
                var result = await _service.CreateAsync(categoryCreateViewModel.Category, UserId);
                if (result == null)
                    return BadRequest(ModelState);

                LoggingWarning($"Category {result.Id} created with success");
                return RedirectToAction("Index", "Category");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpGet("Categories/Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                GetClaims();
                var categoryUpdateViewModel = new CategoryUpdateViewModel();
                categoryUpdateViewModel.Category = await _service.FindByIdUpdateAsync(id);
                categoryUpdateViewModel.Categories = await _service.FindAsyncAllCommonAndUserCategories(UserId);
                return View(categoryUpdateViewModel);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost("Categories/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, CategoryUpdateViewModel categoryUpdateView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.UpdateAsync(categoryUpdateView.Category);
                if (result == null)
                    return BadRequest(ModelState);

                LoggingWarning($"Category {result.Id} updated with success");
                return RedirectToAction("Index", "Category");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.DeleteAsync(id);
                if (result.Equals(null))
                    return BadRequest(ModelState);

                LoggingWarning($"Category {id} deleted with success");
                return RedirectToAction("Index", "Category");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    }
}
