using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Web.Controllers;

public class BaseController<T> : Controller
{
    protected readonly IServiceProvider _serviceProvider;
    private readonly ILogger<T> _logger;

    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }

    public BaseController(IServiceProvider serviceProvider, ILogger<T> logger)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public void LoggingExceptions(Exception exception) =>
        _logger.LogError(exception: exception.InnerException, message: exception.Message);

    protected void LoggingWarning(string message) => _logger.LogWarning(message);

    protected S GetService<S>() => _serviceProvider.GetService<S>();

    /// <summary>
    /// Get session variables
    /// </summary>
    protected void GetClaims()
    {
        if (User != null && User.Identity.IsAuthenticated)
        {
            UserName = User.Identity.Name;
            UserEmail = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value;
            UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
        }
    }

}
