using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class BaseController<T> : Controller
    {
        private readonly ILogger<T> _logger;

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LoggingExceptions(Exception exception) => 
            _logger.LogError(exception: exception.InnerException, message: exception.Message);

        public void LoggingWarning(string message) => _logger.LogWarning(message);

        public void GetClaims()
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                UserName = User.Identity.Name;
                UserEmail = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value;
                UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
            }
        }

    }
}
