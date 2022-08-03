using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    public int CourseId { get; set;}
    public string Name { get; set; }
    public int Number { get; set; }
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    public virtual ICollection<Enrollment> JoinEntities { get; set; }

    public Course()
    {
      this.JoinEntities = new HashSet<Enrollment>();
    }
  }
}