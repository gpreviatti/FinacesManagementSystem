using System;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace Web.Controllers
{
    public class BaseController<T> : Controller
    {
        private readonly ILogger<T> _logger;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LoggingExceptions(Exception exception) => 
            _logger.LogError(exception: exception.InnerException, message: exception.Message);

        public void LoggingWarning(string message) => _logger.LogWarning(message);

    }
}
