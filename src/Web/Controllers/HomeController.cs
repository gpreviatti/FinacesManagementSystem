using System;
using System.Diagnostics;
using Domain.Dtos.Wallet;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IEntranceService _entraceService;
        private readonly IWalletService _walletService;

        public HomeController(IEntranceService entraceService, IWalletService walletService, ILogger<HomeController> logger) : base(logger)
        {
            _entraceService = entraceService;
            _walletService = walletService;
        }

        public IActionResult Index()
        {
            try
            {
                var walletIndexDto = new WalletTotalValuesAndEntrancesDto();
                walletIndexDto = _walletService.WalletsTotalValuesAndLastTenEntrances().Result;
                return View(walletIndexDto);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
