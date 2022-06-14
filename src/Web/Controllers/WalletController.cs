using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Domain.Interfaces.Services;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ExcludeFromCodeCoverage]
    public class WalletController : BaseController<WalletController>
    {
        private readonly IWalletService _service;
        private readonly IWalletTypeService _walletTypeService;

        public WalletController(IServiceProvider serviceProvider, ILogger<WalletController> logger) : 
            base(serviceProvider, logger)
        {
            _service = GetService<IWalletService>();
            _walletTypeService = GetService<IWalletTypeService>();
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                GetClaims();
                
                var wallets = await _service.FindAsyncWalletsUser(UserId);
                
                return View(wallets);
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
                var walletCreateViewModel = new WalletCreateViewModel
                {
                    WalletTypes = await _walletTypeService.FindAllAsync()
                };
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
                return BadRequest(ModelState);

            try
            {
                GetClaims();

                var result = _service.CreateAsync(walletCreateViewModel.Wallet, UserId);

                if (result == null)
                    return BadRequest(ModelState);

                LoggingWarning($"Wallet {result.Id} created with success");
                return RedirectToAction("Index", "Wallet");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var walletUpdateViewModel = new WalletUpdateViewModel
                {
                    Wallet = await _service.FindByIdUpdateAsync(id),
                    WalletTypes = await _walletTypeService.FindAllAsync()
                };

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
        public async Task<ActionResult> Edit(Guid id, WalletUpdateViewModel walletUpdateViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.UpdateAsync(walletUpdateViewModel.Wallet);
                
                if (result == null)
                    return BadRequest(ModelState);

                LoggingWarning($"Wallet {result.Id} updated with success");
                
                return RedirectToAction("Index", "Wallet");
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
