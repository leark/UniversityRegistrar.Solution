using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniversityRegistrar.Controllers
{
  public class CoursesController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public CoursesController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "View All Courses";
      List<Course> model = _db.Courses.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Create a Course";
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Course course)
    {
      _db.Courses.Add(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      var thisDepartment = _db.Departments.FirstOrDefault(dataRow => dataRow.DepartmentId == thisCourse.DepartmentId);
      
      (Course course, Department department) model = (thisCourse, thisDepartment);
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
      return View(model);
    }

    [HttpPost]
    public ActionResult AddStudentToCourse(Enrollment enrollment)
    {
      if (_db.Enrollments.FirstOrDefault(
              e => e.StudentId == enrollment.StudentId && 
                    e.CourseId == enrollment.CourseId) == null)
      {
        _db.Enrollments.Add(enrollment);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = enrollment.CourseId });
    }

    public ActionResult Edit(int id)
    {
      Course courseFound = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      ViewBag.PageTitle = "Edit Course Name";
      return View(courseFound);
    }

    [HttpPost]
    public ActionResult Edit(Course course)
    {
      _db.Entry(course).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    
  }
}

