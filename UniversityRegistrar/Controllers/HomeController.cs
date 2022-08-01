using Microsoft.AspNetCore.Mvc;

namespace UniversityRegistrar.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      ViewBag.PageTitle = "University Registrar Home";
      return View();
    }
  }
}