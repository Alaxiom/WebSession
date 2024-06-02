using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSession.Models;

namespace WebSession.Controllers;
public class HomeController : Controller
{
    public const string SessionKeyName = "_Name";
    public const string SessionKeyAge = "_Age";

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {        
        if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
        {
            HttpContext.Session.SetString(SessionKeyName, "The Doctor");
            HttpContext.Session.SetInt32(SessionKeyAge, 73);
        }

        var name = HttpContext.Session.GetString(SessionKeyName);
        var age = HttpContext.Session.GetInt32(SessionKeyAge).ToString();

        _logger.LogInformation($"SessionId: {HttpContext.Session.Id}");
        _logger.LogInformation("Session Name: {Name}", name);
        _logger.LogInformation("Session Age: {Age}", age);

        return View();
    }

    public IActionResult Privacy()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
        {
            HttpContext.Session.SetString(SessionKeyName, "The Doctor");
            HttpContext.Session.SetInt32(SessionKeyAge, 73);
        }

        var name = HttpContext.Session.GetString(SessionKeyName);
        var age = HttpContext.Session.GetInt32(SessionKeyAge).ToString();

        _logger.LogInformation($"SessionId: {HttpContext.Session.Id}");
        _logger.LogInformation("Session Name: {Name}", name);
        _logger.LogInformation("Session Age: {Age}", age);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}