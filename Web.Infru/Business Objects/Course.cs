using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Infru.Business_Objects
{
    public  class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public String Fees { get; set; }

        public DateTime ClassStartDate { get; set; }

        //input chekinger code gulo ekhane thakbe,method ekhan thekei call hobe ,ekhane declare hobe
        public void SetProperClassStartDate()
        {
            if(ClassStartDate.Subtract(DateTime.Now).TotalDays <30)
            {

            }
             
        }
    }
}
