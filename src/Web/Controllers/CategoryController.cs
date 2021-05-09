using System;
using System.Linq;
using Domain.Dtos.Category;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.ViewModels.Category;

namespace Web.Controllers
{
    public class CategoryController : BaseController<CategoryController>
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service, ILogger<CategoryController> logger) : base(logger)
        {
            _service = service;
        }

        public IActionResult Index() => View();

        [HttpPost("Categories/Datatables")]
        public IActionResult GetEntrancesDatatables(DatatablesModel<CategoryResultDto> datatablesModel)
        {
            try
            {
                datatablesModel.Draw = Request.Form["draw"].FirstOrDefault();
                datatablesModel.Start = Request.Form["start"].FirstOrDefault();
                datatablesModel.Length = Request.Form["length"].FirstOrDefault();
                datatablesModel.SortColumn = int.Parse(Request.Form["order[0][column]"].FirstOrDefault());
                datatablesModel.SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                datatablesModel.SearchValue = Request.Form["search[value]"].FirstOrDefault();

                datatablesModel = _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModel).Result;
                return Ok(datatablesModel);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        public ActionResult Create()
        {
            try
            {
                var categoryCreateViewModel = new CategoryCreateViewModel();
                categoryCreateViewModel.Category = new CategoryCreateDto();
                categoryCreateViewModel.Categories = _service.FindAsyncAllCommonAndUserCategories().Result;
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
        public ActionResult Create(CategoryCreateViewModel categoryCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _service.CreateAsync(categoryCreateViewModel.Category).Result;
                if (result == null)
                {
                    return BadRequest(ModelState);
                }
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
        public ActionResult Edit(Guid id)
        {
            try
            {
                var categoryUpdateViewModel = new CategoryUpdateViewModel();
                categoryUpdateViewModel.Category = _service.FindByIdUpdateAsync(id).Result;
                categoryUpdateViewModel.Categories = _service.FindAsyncAllCommonAndUserCategories().Result;
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
        public ActionResult Edit(Guid id, CategoryUpdateViewModel categoryUpdateView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _service.UpdateAsync(categoryUpdateView.Category).Result;
                if (result == null)
                {
                    return BadRequest(ModelState);
                }
                LoggingWarning($"Category {result.Id} updated with success");
                return RedirectToAction("Index", "Category");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        public ActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _service.DeleteAsync(id).Result;
                if (result.Equals(null))
                {
                    return BadRequest(ModelState);
                }
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
