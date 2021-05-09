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
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
            
        }

        public ActionResult Create()
        {
            try
            {
                var walletCreateViewModel = new WalletCreateViewModel();
                walletCreateViewModel.Wallet = new WalletCreateDto();
                walletCreateViewModel.WalletTypes = _walletTypeService.FindAllAsync().Result;
                return View(walletCreateViewModel);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WalletCreateViewModel walletCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _service.CreateAsync(walletCreateViewModel.Wallet).Result;
                if (result == null)
                {
                    return BadRequest(ModelState);
                }
                LoggingWarning($"Wallet {result.Id} created with success");
                return RedirectToAction("Index", "Wallet");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        public ActionResult Edit(Guid id)
        {
            try
            {
                var walletUpdateViewModel = new WalletUpdateViewModel();
                walletUpdateViewModel.Wallet = _service.FindByIdUpdateAsync(id).Result;
                walletUpdateViewModel.WalletTypes = _walletTypeService.FindAllAsync().Result;
                return View(walletUpdateViewModel);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, WalletUpdateViewModel walletUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _service.UpdateAsync(walletUpdateViewModel.Wallet).Result;
                if (result == null)
                {
                    return BadRequest(ModelState);
                }
                LoggingWarning($"Wallet {result.Id} updated with success");
                return RedirectToAction("Index", "Wallet");
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
                LoggingWarning($"Wallet {id} deleted with success");
                return RedirectToAction("Index", "Wallet");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    }
}
