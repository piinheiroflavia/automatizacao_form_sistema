using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SMAP.Models;

namespace SMAP.Controllers;

public class Page2 : Controller
{
  public IActionResult Index() => View();
}
