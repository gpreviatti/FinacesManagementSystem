using System;
using System.Collections.Generic;
using System.Diagnostics;
using Domain.Dtos.EntranceTypeDto;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.ViewModels.Home;

namespace Web.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IEntranceService _entraceService;
        private readonly IWalletService _walletService;

        public HomeController(
            IEntranceService entraceService, 
            IWalletService walletService,
            ILogger<HomeController> logger
        ) : base(logger)
        {
            _entraceService = entraceService;
            _walletService = walletService;
        }

        public IActionResult Index()
        {
            try
            {
                var homeIndexViewModel = new HomeIndexViewModel();
                homeIndexViewModel.Entrances = _entraceService.FindAsyncLastFiveEntrancesWithCategories().Result;
                homeIndexViewModel.Wallets = _walletService.FindAsyncWalletsUser().Result;
                homeIndexViewModel.TotalExpanse = 1000;
                homeIndexViewModel.TotalIncome = 5000;

                return View(homeIndexViewModel);
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
