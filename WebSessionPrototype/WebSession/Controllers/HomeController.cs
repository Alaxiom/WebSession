using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using WebSession.Models;

namespace WebSession.Controllers;
public class HomeController : Controller
{
    public const string SessionKeyName = "_Name";
    public const string SessionKeyAge = "_Age";

    public const string CacheKeyName = "_Name";

    private readonly ILogger<HomeController> _logger;
    private readonly IDistributedCache _cache;

    public HomeController(
        ILogger<HomeController> logger,
        IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public async Task<IActionResult> Index()
    {        
        if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
        {
            HttpContext.Session.SetString(SessionKeyName, "The Doctor");
            HttpContext.Session.SetInt32(SessionKeyAge, 73);
        }
        
        if(!string.IsNullOrEmpty(HttpContext.Session.GetString(CacheKeyName)))        
        {            
            await _cache.SetStringAsync(CacheKeyName, "Kim Philby");
        }
        

        var name = HttpContext.Session.GetString(SessionKeyName);
        var age = HttpContext.Session.GetInt32(SessionKeyAge).ToString();
        var cachedName = await _cache.GetStringAsync(CacheKeyName);

        _logger.LogInformation($"SessionId: {HttpContext.Session.Id}");
        _logger.LogInformation($"Session Name: {name}");
        _logger.LogInformation($"Session Age: {age}");
        _logger.LogInformation($"Cached Name: {cachedName}");

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
