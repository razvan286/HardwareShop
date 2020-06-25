using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar
{
    public class RestockRequest : RestockRequestBase
    {
        public int RequestID { get; set; }
        public string Brand { get; set; }
        public int EmployeeID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Departament { get; set; }
        public int Quantity { get; set; }
        public string DateOfOrder { get; set; }
        public string DateOfDelivery { get; set; }
        public string AdminConfirmation { get; set; }
        public string ExtraInfo { get; set; }

        public string FullInfo
        {
            get
            {
                return base.FullInfo;
            }
        }

        public string Info
        {
            get
            {
                return "ID:" + this.RequestID + " " + this.ProductName + " " + this.Brand ;
            }
        }
    }
}
