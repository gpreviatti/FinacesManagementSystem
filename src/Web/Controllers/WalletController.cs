using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletService _service;
        public WalletController(IWalletService service)
        {
            _service = service;
        }

        // GET: WalletController
        public ActionResult Index()
        {
            var wallets = _service.FindAllAsync().Result;
            return View(wallets);
        }

        // GET: WalletController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalletController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalletController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalletController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalletController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalletController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalletController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
