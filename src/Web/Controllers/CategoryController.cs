using System;
using System.Linq;
using Domain.Dtos.Category;
using Domain.Interfaces.Services;
using Domain.Models;
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
            datatablesModel.Draw = Request.Form["draw"].FirstOrDefault();
            datatablesModel.Start = Request.Form["start"].FirstOrDefault();
            datatablesModel.Length = Request.Form["length"].FirstOrDefault();
            datatablesModel.SortColumn = int.Parse(Request.Form["order[0][column]"].FirstOrDefault());
            datatablesModel.SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            datatablesModel.SearchValue = Request.Form["search[value]"].FirstOrDefault();

            datatablesModel = _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModel).Result;
            return Ok(datatablesModel);
        }

        public ActionResult Create()
        {
            var categoryCreateViewModel = new CategoryCreateViewModel();
            categoryCreateViewModel.Category = new CategoryCreateDto();
            categoryCreateViewModel.Categories = _service.FindAsyncAllCommonAndUserCategories().Result;
            return View(categoryCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreateViewModel categoryCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.CreateAsync(categoryCreateViewModel.Category).Result;
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return RedirectToAction("Index", "Category");
        }

        [HttpGet("Categories/Edit/{id}")]
        public ActionResult Edit(Guid id)
        {
            var categoryUpdateViewModel = new CategoryUpdateViewModel();
            categoryUpdateViewModel.Category = _service.FindByIdUpdateAsync(id).Result;
            categoryUpdateViewModel.Categories = _service.FindAsyncAllCommonAndUserCategories().Result;
            return View(categoryUpdateViewModel);
        }

        [HttpPost("Categories/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, CategoryUpdateViewModel categoryUpdateView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.UpdateAsync(categoryUpdateView.Category).Result;
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return RedirectToAction("Index", "Category");
        }

        public ActionResult Delete(Guid id)
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
            return RedirectToAction("Index", "Category");
        }
    }
}
