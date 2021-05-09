using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class BaseController<T> : Controller
    {
        private readonly ILogger<T> _logger;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LoggingExceptions(Exception exception)
        {
            _logger.LogError(exception: exception.InnerException, message: exception.Message);
        }
    }
}
