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

    public ActionResult Details(int id)
    {
      Department department = _db.Departments.FirstOrDefault(d => d.DepartmentId == id);
      return View(department);
    }

    public ActionResult Edit(int id)
    {
      var thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
      return View(thisDepartment);
    }

    [HttpPost]
    public ActionResult Edit(Department department)
    {
      _db.Entry(department).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Department department = _db.Departments.FirstOrDefault(d => d.DepartmentId == id);
      return View(department);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult Deleted(int id)
    {
      Department department = _db.Departments.FirstOrDefault(d => d.DepartmentId == id);
      _db.Departments.Remove(department);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}