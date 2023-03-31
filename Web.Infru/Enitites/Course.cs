using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Infru.Enitites
{
    public class Course:IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public String Fees { get; set; }

        public DateTime ClassStartDate { get; set; }
        public List<Topic> Topics { get; set; }
        public List<CourseRegistration> CourseStudents { get; set; }


    }
}
