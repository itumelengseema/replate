using Microsoft.AspNetCore.Mvc;

namespace Replate.Api.Controllers;

public class VendorsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}