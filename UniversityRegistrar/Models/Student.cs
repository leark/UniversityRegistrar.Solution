using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Student
  {
    public int StudentId { get; set;}
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    public virtual ICollection<Enrollment> JoinEntities { get; set; }
    
    public Student()
    {
      this.JoinEntities = new HashSet<Enrollment>();
    }
  }
}