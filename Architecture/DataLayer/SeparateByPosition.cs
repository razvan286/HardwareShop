using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar
{
    public class SeparateByPosition
    {
        DataAccess dataAccess = new DataAccess();

        List<EmployeeBase> nrAdmins = new List<EmployeeBase>();
        List<EmployeeBase> nrManagers = new List<EmployeeBase>();
        List<EmployeeBase> nrDepotWorkers = new List<EmployeeBase>();
        List<EmployeeBase> nrEmployees = new List<EmployeeBase>();

        public void SeparateEmployeesByPosition(List<EmployeeBase> employeesForShift)
        {
            foreach (EmployeeBase employee in employeesForShift)
            {
                if (dataAccess.GetPositionByID(employee.EmployeeID) == "ADMINISTRATOR")
                {
                    nrAdmins.Add(employee);
                }
                else if (dataAccess.GetPositionByID(employee.EmployeeID) == "MANAGER")
                {
                    nrManagers.Add(employee);
                }
                else if (dataAccess.GetPositionByID(employee.EmployeeID) == "DEPOT")
                {
                    nrDepotWorkers.Add(employee);
                }
                else if (dataAccess.GetPositionByID(employee.EmployeeID) == "EMPLOYEE")
                {
                    nrEmployees.Add(employee);
                }
            }
        }

        public List<EmployeeBase> GetAdmins()
        {
            return nrAdmins;
        }

        public List<EmployeeBase> GetManagers()
        {
            return nrManagers;
        }

        public List<EmployeeBase> GetDepotWorkers()
        {
            return nrDepotWorkers;
        }

        public List<EmployeeBase> GetEmployees()
        {
            return nrEmployees;
        }
    }
}
