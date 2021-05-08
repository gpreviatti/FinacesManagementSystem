using System.Collections.Generic;
using System.Diagnostics;
using Domain.Dtos.EntranceTypeDto;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.ViewModels.Home;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEntranceService _entraceService;
        private readonly IWalletService _walletService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IEntranceService entraceService, IWalletService walletService)
        {
            _entraceService = entraceService;
            _walletService = walletService;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            var homeIndexViewModel = new HomeIndexViewModel();
            homeIndexViewModel.Entrances = _entraceService.FindAsyncLastFiveEntrancesWithCategories().Result;
            homeIndexViewModel.Wallets = _walletService.FindAsyncWalletsUser().Result;
            homeIndexViewModel.TotalExpanse = 1000;
            homeIndexViewModel.TotalIncome = 5000;

            return View(homeIndexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
