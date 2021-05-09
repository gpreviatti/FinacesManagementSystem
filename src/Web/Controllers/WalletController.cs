using System;
using Domain.Dtos.Wallet;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.ViewModels.Wallet;

namespace Web.Controllers
{
    public class WalletController : BaseController<WalletController>
    {
        private readonly IWalletService _service;
        private readonly IWalletTypeService _walletTypeService;

        public WalletController(
            IWalletService service, 
            IWalletTypeService walletTypeService,
            ILogger<WalletController> logger
        ) : base(logger)
        {
            _service = service;
            _walletTypeService = walletTypeService;
        }

        public ActionResult Index()
        {
            try
            {
                var wallets = _service.FindAsyncWalletsUser().Result;
                return View(wallets);
            }
            catch (Exception exception)
            {
                LoggingExceptions(_logger, exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
            
        }

        public ActionResult Create()
        {
            var walletCreateViewModel = new WalletCreateViewModel();
            walletCreateViewModel.Wallet = new WalletCreateDto();
            walletCreateViewModel.WalletTypes = _walletTypeService.FindAllAsync().Result;
            return View(walletCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WalletCreateViewModel walletCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.CreateAsync(walletCreateViewModel.Wallet).Result;
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return RedirectToAction("Index", "Wallet");
        }

        public ActionResult Edit(Guid id)
        {
            var walletUpdateViewModel = new WalletUpdateViewModel();
            walletUpdateViewModel.Wallet = _service.FindByIdUpdateAsync(id).Result;
            walletUpdateViewModel.WalletTypes = _walletTypeService.FindAllAsync().Result;
            return View(walletUpdateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, WalletUpdateViewModel walletUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.UpdateAsync(walletUpdateViewModel.Wallet).Result;
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return RedirectToAction("Index", "Wallet");
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
            return RedirectToAction("Index", "Wallet");
        }
    }
}
