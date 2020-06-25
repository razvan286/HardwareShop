using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar.LogicLayer.Algorithms
{
    public class NewGenerator
    {
        List<EmployeeBase> employeesWithPreferences = new List<EmployeeBase>();
        DataAccess dataAccess = new DataAccess();

        //all employees who expressed preferences for shifts
        private List<EmployeeBase> CheckEmployeesWhichCanWork(string date, string department)
        {
            List<EmployeeBase> employees = new List<EmployeeBase>();
            foreach (EmployeeBase employee in dataAccess.GetEmployeesPreferencesForDayByDepartment(date, department))
            {
                if (dataAccess.GetMaxHoursByID(employee.EmployeeID) > dataAccess.GetCurrentHoursByID(employee.EmployeeID))
                {
                    employees.Add(employee);
                }
            }
            return employees;
        }
        private List<EmployeeBase> CheckEmployeesWithoutPreferences(string department)
        {
            List<EmployeeBase> employees = new List<EmployeeBase>();
            return employees;
        }
        string firstShiftPeriod = "";
        string secondShiftPeriod = "";
        string thirdShiftPeriod = "";

        //all employees with preferences
        List<EmployeeBase> adminsWithPreference = new List<EmployeeBase>();
        List<EmployeeBase> managerWithPreference = new List<EmployeeBase>();
        List<EmployeeBase> depotWithPreference = new List<EmployeeBase>();
        List<EmployeeBase> employeeWithPreference = new List<EmployeeBase>();

        //all employees without preferences
        List<EmployeeBase> adminsWithoutPreference = new List<EmployeeBase>();
        List<EmployeeBase> managerWithoutPreference = new List<EmployeeBase>();
        List<EmployeeBase> depotWithoutPreference = new List<EmployeeBase>();
        List<EmployeeBase> employeeWithoutPreference = new List<EmployeeBase>();
        public void EmployeesWithPreferencesPerDay(List<EmployeeBase> employees)
        {
            foreach(EmployeeBase employee in employees)
            {
                if (employee.Position == "ADMINISTRATOR")
                    adminsWithPreference.Add(employee);
                if (employee.Position == "MANAGER")
                    managerWithPreference.Add(employee);
                if (employee.Position == "DEPOT")
                    depotWithPreference.Add(employee);
                if (employee.Position == "EMPLOYEE")
                    employeeWithPreference.Add(employee);
            }
        }
        public void EmployeesWithoutPreferencesPerDay(List<EmployeeBase> employees, string department)
        {
            //get all employees from department
            List<EmployeeBase> employeesWithoutPref = new List<EmployeeBase>();
           // List<EmployeeBase> allEmployees = dataAccess.GetEmployeesByDepartment(department);

            //get all employees which have not expressed preference
            //var employeesWithoutPref2 = allEmployees.Where(e => employees.All(e2 => e2.EmployeeID != e.EmployeeID));
          //  employeesWithoutPref = employeesWithoutPref2.ToList();
            foreach(EmployeeBase employee in employeesWithoutPref)
            {
                if (employee.Position == "ADMINISTRATOR")
                    adminsWithoutPreference.Add(employee);
                if (employee.Position == "MANAGER")
                    managerWithoutPreference.Add(employee);
                if (employee.Position == "DEPOT")
                    depotWithoutPreference.Add(employee);
                if (employee.Position == "EMPLOYEE")
                    employeeWithoutPreference.Add(employee);
            }
        }
        List<EmployeeBase> firstShiftAdmin = new List<EmployeeBase>();
        List<EmployeeBase> secondShiftAdmin = new List<EmployeeBase>();
        List<EmployeeBase> thirdShiftAdmin = new List<EmployeeBase>();

        List<EmployeeBase> firstShiftManager = new List<EmployeeBase>();
        List<EmployeeBase> secondShiftManager = new List<EmployeeBase>();
        List<EmployeeBase> thirdShiftManager = new List<EmployeeBase>();

        List<EmployeeBase> firstShiftDepot = new List<EmployeeBase>();
        List<EmployeeBase> secondShiftDepot = new List<EmployeeBase>();
        List<EmployeeBase> thirdShiftDepot = new List<EmployeeBase>();

        List<EmployeeBase> firstShiftEmployee = new List<EmployeeBase>();
        List<EmployeeBase> secondShiftEmployee = new List<EmployeeBase>();
        List<EmployeeBase> thirdShiftEmployee = new List<EmployeeBase>();
        public void AssignEmployeesToShift(int nrOfAdminsNeededPerShift, int nrOfManagersNeededPerShift, int nrOfDepotNeededPerShift, int nrOfEmployeesNeededPerShift, string department)
        {
            DateTime today = DateTime.Today;
            /*List<EmployeeBase> newListEmployeesPref = new List<EmployeeBase>();
            foreach(EmployeeBase employee in employeesWithPreferences)
            {
                if(employee.Departament == department)
                {
                    //store only employees from a specific department
                    newListEmployeesPref.Add(employee);
                }
            } */
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                for (int i = 0; i < 7; i++)
                {
                    //date today.AddDays(i)
                    string date = today.AddDays(i).ToString("dd/MM/yyyy");
                    employeesWithPreferences = CheckEmployeesWhichCanWork(date, department);
                    //weekdays
                    if(i < 5)
                    {
                        firstShiftPeriod = "07:00-12:00";
                        secondShiftPeriod = "12:00-17:00";
                        thirdShiftPeriod = "17:00-22:00";
                        //get the number of admins, managers, depots, employees who expressed preferences for this day
                        //compare with the number needed
                        EmployeesWithPreferencesPerDay(employeesWithPreferences);
                        EmployeesWithoutPreferencesPerDay(employeesWithPreferences, department);
                        //make list with all admins with preferences
                        //make list with all admins without preferences
                        //do this for each type of employee
                        foreach(EmployeeBase employee in employeesWithPreferences)
                        {
                            if (dataAccess.GetShiftPreferences(date, employee.EmployeeID) == firstShiftPeriod)
                            {
                                if(employee.Position == "ADMINISTRATOR")//admins prefering first shift
                                    firstShiftAdmin.Add(employee);
                                if (employee.Position == "MANAGER")
                                    firstShiftManager.Add(employee);
                                if (employee.Position == "DEPOT")
                                    firstShiftDepot.Add(employee);
                                if (employee.Position == "EMPLOYEE")
                                    firstShiftEmployee.Add(employee);
                            }
                            else if(dataAccess.GetShiftPreferences(date, employee.EmployeeID) == secondShiftPeriod)
                            {
                                if (employee.Position == "ADMINISTRATOR")//admins prefering second shift
                                    secondShiftAdmin.Add(employee);
                                if (employee.Position == "MANAGER")
                                    secondShiftManager.Add(employee);
                                if (employee.Position == "DEPOT")
                                    secondShiftDepot.Add(employee);
                                if (employee.Position == "EMPLOYEE")
                                    secondShiftEmployee.Add(employee);
                            }
                            else if(dataAccess.GetShiftPreferences(date, employee.EmployeeID) == thirdShiftPeriod)
                            {
                                if (employee.Position == "ADMINISTRATOR")//admins prefering second shift
                                    thirdShiftAdmin.Add(employee);
                                if (employee.Position == "MANAGER")
                                    thirdShiftManager.Add(employee);
                                if (employee.Position == "DEPOT")
                                    thirdShiftDepot.Add(employee);
                                if (employee.Position == "EMPLOYEE")
                                    thirdShiftEmployee.Add(employee);
                            }
                        }
                        
                    }
                    else if(i == 5)
                    {//saturday
                        firstShiftPeriod = "09:00-15:00";
                        secondShiftPeriod = "15:00-18:00";
                    }
                    else
                    {//sunday
                        firstShiftPeriod = "12:00-18:00";
                    }

                }
            }
            else
            {
                DateTime tomorrow = DateTime.Today.AddDays(1);
                // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
                int daysUntilMonday = ((int)DayOfWeek.Monday - (int)tomorrow.DayOfWeek + 7) % 7;
                DateTime nextMonday = tomorrow.AddDays(daysUntilMonday);

                for (int i = 0; i < 7; i++)
                {
                    string date = nextMonday.AddDays(i).Date.ToString("dd/MM/yyyy");
                }
            }
        }
    }
}
