using System;
using Domain.Dtos.Category;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Category;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var categories = _service.FindAsyncAllCommonAndUserCategories().Result;
            return View(categories);
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

        public ActionResult Edit(Guid id)
        {
            var categoryUpdateViewModel = new CategoryUpdateViewModel();
            categoryUpdateViewModel.Category = _service.FindByIdUpdateAsync(id).Result;
            categoryUpdateViewModel.Categories = _service.FindAsyncAllCommonAndUserCategories().Result;
            return View(categoryUpdateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryUpdateViewModel categoryUpdateView)
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
