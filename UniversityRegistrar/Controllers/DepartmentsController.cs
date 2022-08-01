using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniversityRegistrar.Controllers
{
  public class DepartmentsController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public DepartmentsController(UniversityRegistrarContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      ViewBag.PageTitle = "View All Departments";
      List<Department> model = _db.Departments.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Add a Department";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Department department)
    {
      _db.Departments.Add(department);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}