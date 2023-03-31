using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Infru.Enitites
{
    public class CourseRegistration
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
