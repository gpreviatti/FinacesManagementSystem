using System.Collections.Generic;
using System.Diagnostics;
using Domain.Dtos.EntraceTypeDto;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.ViewModels.Home;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEntraceService _entraceService;
        private readonly IWalletService _walletService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IEntraceService entraceService, IWalletService walletService, ICategoryService categoryService)
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
            homeIndexViewModel.Entraces = _entraceService.FindAsyncLastTenEntracesWithCategories().Result;
            homeIndexViewModel.Wallets = _walletService.FindAllAsync().Result;
            homeIndexViewModel.TotalExpanse = 1000;
            homeIndexViewModel.TotalIncome = 5000;

            return View(homeIndexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
