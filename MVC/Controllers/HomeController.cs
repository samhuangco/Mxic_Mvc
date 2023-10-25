using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;


namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult AccInfo()
    { 
        return View();
    }

    [HttpPost]
    public ActionResult AccInfo(IFormCollection FormData)
    {
        string Name = FormData["Name"].ToString();
        string Age = FormData["Age"].ToString();
        string Brithday = FormData["Brithday"].ToString();

        dynamic obj = new System.Dynamic.ExpandoObject();

        obj.Name = Name;
        obj.Age = Age;
        obj.Brithday = Brithday;
        string Jsonstr = JsonSerializer.Serialize(obj);

        string Key = Guid.NewGuid().ToString("N");

        HttpContext.Session.SetString("Accinfo-" + Key, Jsonstr);

        return View("AccInfo");
    }

    [HttpPut]
    public IActionResult AccInfo(string Id,string Json)
    {

        HttpContext.Session.SetString(Id, Json);

        return PartialView("_AccDataPartial");
    }

    [HttpDelete]
    public IActionResult AccInfo(string Id)
    {
        HttpContext.Session.Remove(Id);

        return PartialView("_AccDataPartial");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

