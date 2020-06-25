using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Media_Bazaar
{
    public class ManagerManagment
    {
        List<EmployeeBase> employees = new List<EmployeeBase>();
        List<RestockRequestBase> restocks = null;
        List<EmployeeBase> allEmployees = null;
        List<Schedule> schedules = null;

        DataAccess dataAccess = new DataAccess();

        public void UpdateData(int id, string dateOfBirth, string nationality, string email, string phoneNumber, string password)
        {
            dataAccess.UpdateEmployee(id, dateOfBirth, nationality, email, phoneNumber, password);
        }

        public List<EmployeeBase> GetEmployeesByLName(string lastName)
        {
            return dataAccess.GetDBEmployeesByLastName(lastName);
        }

        public List<EmployeeBase> GetNotFiredEmployeesByLName(string lastName)
        {
            return dataAccess.GetNotFiredEmployeesByLastName(lastName);
        }

        public List<EmployeeBase> GetNotFiredEmployeesByID(int id)
        {
            return dataAccess.GetNotFiredEmployeesByID(id);
        }

        public List<ScheduleBase> GetSchedulesByEmplId(int id)
        {
            return dataAccess.GetSchedulesByEmplId(id);
        }

        public List<EmployeeBase> GetAllEmployees()
        {
            return dataAccess.GetAllEmployees();
        }

        public List<ScheduleBase> GetAllSchedules()
        {
            return dataAccess.GetAllSchedules();
        }

        public int GetNumberOfAbsentByID(int id)
        {
            return dataAccess.GetNumOfAbsentById(id);
        }

        public int GetNumberOfPresentByID(int id)
        {
            return dataAccess.GetNumOfPresentById(id);
        }

        public int GetNumberOfLateByID(int id)
        {
            return dataAccess.GetNumOfLateById(id);
        }

        public List<RestockRequestBase> GetAllRestockRequests()
        {
            return dataAccess.GetAllRequests();
        }

        public List<RestockRequestBase> GetAllRestockNames()
        {
            return dataAccess.GetRequestsName();
        }

        //FOR CHARTS
        public int NumberrOfWorking()
        {
            int nrWorking = 0;

            foreach (EmployeeBase employee in GetAllEmployees())
            {
                if (employee.ReasonsForRelease == null)
                {
                    nrWorking++;
                }
            }
            return nrWorking;
        }

        public int NumberOfFired()
        {
            int nrFired = 0;

            foreach (EmployeeBase employee in GetAllEmployees())
            {
                if (employee.ReasonsForRelease != null)
                {
                    nrFired++;
                }
            }
            return nrFired;
        }

        public int NumberOfAdministrators()
        {
            int nrAdmins = 0;

            foreach (EmployeeBase employee in GetAllEmployees())
            {
                if (employee.Position == "ADMINISTRATOR")
                {
                    nrAdmins++;
                }
            }
            return nrAdmins;
        }

        public int NumberOfManagers()
        {
            int nrManagers = 0;

            foreach (EmployeeBase employee in GetAllEmployees())
            {
                if (employee.Position == "MANAGER")
                {
                    nrManagers++;
                }
            }
            return nrManagers;
        }

        public int NumberOfDepotWorkers()
        {
            int nrDepotWorkers = 0;

            foreach (EmployeeBase employee in GetAllEmployees())
            {
                if (employee.Position == "DEPOT")
                {
                    nrDepotWorkers++;
                }
            }
            return nrDepotWorkers;
        }

        public int NumberOfPresent()
        {
            int nrOfPresent = 0;

            foreach (ScheduleBase sch in GetAllSchedules())
            {
                if (sch.Attendance == "PRESENT")
                {
                    nrOfPresent++;
                }
            }
            return nrOfPresent;
        }

        public int NumberOfLate()
        {
            int nrOfLate = 0;
            foreach (ScheduleBase sch in GetAllSchedules())
            {
                if (sch.Attendance == "LATE")
                {
                    nrOfLate++;
                }
            }
            return nrOfLate;
        }

        public int NumberOfAbsent()
        {
            int nrOfAbsent = 0;

            foreach (ScheduleBase sch in GetAllSchedules())
            {
                if (sch.Attendance == "ABSENT")
                {
                    nrOfAbsent++;
                }
            }
            return nrOfAbsent;
        }

        public int NumberOfConfirmedRequests()
        {
            int nrOfConfirmed = 0;

            foreach (RestockRequestBase req in GetAllRestockRequests())
            {
                if (req.AdminConfirmation == "CONFIRMED")
                {
                    nrOfConfirmed++;
                }
            }
            return nrOfConfirmed;
        }

        public int NumberOfRejectedRequests()
        {
            int nrOfRejected = 0;

            foreach (RestockRequestBase req in GetAllRestockRequests())
            {
                if (req.AdminConfirmation == "REJECTED")
                {
                    nrOfRejected++;
                }
            }
            return nrOfRejected;
        }

        public int NumberOfWaitingRequests()
        {
            int nrOfWaiting = 0;

            foreach (RestockRequestBase req in GetAllRestockRequests())
            {
                if (req.AdminConfirmation ==null)
                {
                    nrOfWaiting++;
                }
            }
            return nrOfWaiting;
        }

        public int GetRestockQuantity(string name)
        {
            return dataAccess.GetStockQuantityByName(name);
        }
    }
}