using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EIS.model
{
    class EmpInfo
    {
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string email_id { get; set; }
        public string emp_id { get; set; }
        public DateTime dob { get; set; }
        public DateTime doj { get; set; }
        public DateTime dol { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string department { get; set; }
        public int salary { get; set; }
        public string vendor { get; set; }
    }
}
