using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SMAP.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SMAP.Controllers;

[Authorize(Policy = "RequireAuthenticatedUser")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        ViewData["UserName"] = userName;
        return View();
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
