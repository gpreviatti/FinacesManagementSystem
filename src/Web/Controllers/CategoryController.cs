using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Domain.Interfaces.Services;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace Web.Controllers;

[ExcludeFromCodeCoverage]
public class CategoryController : BaseController<CategoryController>
{
    private readonly ICategoryService _service;

    public CategoryController(IServiceProvider serviceProvider, ILogger<CategoryController> logger) : 
        base(serviceProvider, logger) => _service = GetService<ICategoryService>();

    /// <summary>
    /// Index view with paginate
    /// </summary>
    /// <param name="currentSort"></param>
    /// <param name="searchString"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("Category")]
    public ViewResult Index(
        string currentSort,
        string searchString,
        int? page,
        int pageSize = 10
    )
    {
        currentSort = string.IsNullOrEmpty(currentSort) ? "" : currentSort;

        ViewBag.NameSort = currentSort == "name" ? "name_desc" : "name";
        ViewBag.TotalSort = currentSort == "total" ? "total_desc" : "total";
        ViewBag.CreatedAtSort = currentSort == "createdAt" ? "" : "createdAt";

        if (searchString != null)
            page = 1;

        ViewBag.SearchString = searchString;
        ViewBag.PageSize = pageSize;

        GetClaims();

        var categories = _service.FindAllAndUserCategories(
            currentSort,
            searchString,
            UserId
        ).GetAwaiter().GetResult();

        var pageNumber = page ?? 1;

        return View(categories?.ToPagedList(pageNumber, pageSize));
    }

    /// <summary>
    /// Return categories for generate a chart
    /// </summary>
    /// <returns></returns>
    [HttpGet("Categories/Chart")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            GetClaims();

            var categories = await _service.FindAllAndUserCategories("", "", UserId);

            return Ok(categories);
        }
        catch (Exception exception)
        {
            LoggingExceptions(exception);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
        }
    }

    public async Task<ActionResult> Create()
    {
        try
        {
            GetClaims();
            var categoryCreateViewModel = new CategoryCreateViewModel();
            categoryCreateViewModel.Categories = await _service.FindAsyncAllCommonAndUserCategories(UserId);
            return View(categoryCreateViewModel);
        }
        catch (Exception exception)
        {
            LoggingExceptions(exception);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
        }
    }

    /// <summary>
    /// Create category
    /// </summary>
    /// <param name="categoryCreateViewModel"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CategoryCreateViewModel categoryCreateViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            GetClaims();
            
            var result = _service.CreateAsync(categoryCreateViewModel.Category, UserId);
            
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
            var categoryUpdateViewModel = await _service.SetupCategoryUpdateViewModel(id, UserId);
            return View(categoryUpdateViewModel);
        }
        catch (Exception exception)
        {
            LoggingExceptions(exception);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
        }
    }

    /// <summary>
    /// Update a category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="categoryUpdateView"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Delete a category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
