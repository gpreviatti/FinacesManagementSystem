using System;
using Domain.Dtos.Entrace;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class EntraceController : Controller
    {
        private readonly IEntraceService _service;

        public EntraceController(IEntraceService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var entraces = _service.FindAllAsync().Result;
            return View(entraces);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EntraceCreateDto entraceCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.CreateAsync(entraceCreateDto).Result;
            if (result.Equals(null))
            {
                return BadRequest(ModelState);
            }
            return Redirect("/");
        }

        [HttpGet("Edit/{Id}")]
        public IActionResult Edit(Guid id)
        {
            var entrace = _service.FindByIdAsync(id).Result;
            return View(entrace);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EntraceUpdateDto entraceUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.UpdateAsync(entraceUpdateDto).Result;
            if (result.Equals(null))
            {
                return BadRequest(ModelState);
            }
            return Redirect("/");
        }


        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.DeleteAsync(id);
            if (result.Equals(null))
            {
                return BadRequest(ModelState);
            }
            return Redirect("/");
        }
    }
}
