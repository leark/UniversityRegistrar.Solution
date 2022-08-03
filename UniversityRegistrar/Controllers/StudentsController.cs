using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniversityRegistrar.Controllers
{
  public class StudentsController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public StudentsController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "View All Students";
      List<Student> model = _db.Students.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Create a Student";
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student)
    {
      _db.Students.Add(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      // .Include(student => student.JoinEntities)
      // .ThenInclude(join => join.Course)
      // .FirstOrDefault(student => student.StudentId == id);

      var thisDepartment = _db.Departments.FirstOrDefault(dataRow => dataRow.DepartmentId == thisStudent.DepartmentId);
      
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");

      (Student student, Department department) model = (thisStudent, thisDepartment);
      return View(model);
    }

    [HttpPost]
    public ActionResult AddCourseToStudent(Enrollment enrollment)
    {
      if (_db.Enrollments.FirstOrDefault(
              e => e.StudentId == enrollment.StudentId && 
                    e.CourseId == enrollment.CourseId) == null)
      {
        _db.Enrollments.Add(enrollment);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = enrollment.StudentId });
    }

    public ActionResult Edit(int id)
    {
      Student studentFound = _db.Students.FirstOrDefault(student => student.StudentId == id);
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      ViewBag.PageTitle = "Edit Student";
      return View(studentFound);
    }

    [HttpPost]
    public ActionResult Edit(Student student)
    {
      _db.Entry(student).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Student student = _db.Students.FirstOrDefault(s => s.StudentId == id);
      return View(student);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult Deleted(int id)
    {
      Student student = _db.Students.FirstOrDefault(s => s.StudentId == id);
      _db.Students.Remove(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}

