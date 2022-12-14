using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniversityRegistrar.Controllers
{
  public class EnrollmentsController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public EnrollmentsController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "View All Enrollments";
      List<Enrollment> model = _db.Enrollments.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Create an Enrollment";
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Enrollment enrollment)
    {
      if (_db.Enrollments.FirstOrDefault(
              e => e.StudentId == enrollment.StudentId && 
                    e.CourseId == enrollment.CourseId) == null)
      {
        _db.Enrollments.Add(enrollment);
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisEnrollment = _db.Enrollments.FirstOrDefault(e => e.EnrollmentId == id);
      var thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == thisEnrollment.CourseId);
      var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == thisEnrollment.StudentId);
      
      (Enrollment enrollment, Course course, Student student) model = (thisEnrollment, thisCourse, thisStudent);
      return View(model);
    }

    public ActionResult Delete(int id)
    {
      Enrollment enrollment = _db.Enrollments.FirstOrDefault(e => e.EnrollmentId == id);
      return View(enrollment);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult Deleted(int id)
    {
      Enrollment enrollment = _db.Enrollments.FirstOrDefault(e => e.EnrollmentId == id);
      _db.Enrollments.Remove(enrollment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}

