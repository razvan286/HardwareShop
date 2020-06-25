using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar
{
    public class Emails : EmailModel
    {
        public int EmailID { get; private set; }
        public int EmployeeID { get; private set; }
        public string Email { get; private set; }
        public string Date { get; private set; }
        public string Status { get; private set; }
    }
}
