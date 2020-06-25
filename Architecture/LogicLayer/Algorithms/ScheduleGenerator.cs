using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar
{
    public class ScheduleGenerator
    {
        DataAccess dataAccess = new DataAccess();
        private SeparateByPosition separate;

        List<EmployeeBase> employeesWithPreferencesForAssigning = new List<EmployeeBase>();

        List<EmployeeBase> firstShift = new List<EmployeeBase>();
        List<EmployeeBase> secondShift = new List<EmployeeBase>();
        List<EmployeeBase> thirdShift = new List<EmployeeBase>();

        string firstShiftPeriod = "";
        string secondShiftPeriod = "";
        string thirdShiftPeriod = "";

        int nrWorkingHoursFirstShift;
        int nrWorkingHoursSecondShift;
        int nrWorkingHoursThirdShift;


        //checks which employees can still work (didn't exceed their hours) for a certain day (by department)
        private void CheckEmployeesWhichCanWork(DateTime date, string department)
        {
            foreach (EmployeeBase employee in dataAccess.GetEmployeesPreferencesForDayByDepartment(date.ToString("dd/MM/yyyy"), department))
            {
                if (dataAccess.GetMaxHoursByID(employee.EmployeeID) > dataAccess.GetCurrentHoursByID(employee.EmployeeID))
                {
                    employeesWithPreferencesForAssigning.Add(employee);
                }
            }
        }

        private void SeparateTheShiftPerDay(DateTime date)
        {
            if (date.DayOfWeek.ToString() == "Saturday")
            {
                firstShiftPeriod = "09:00-15:00";
                secondShiftPeriod = "15:00-18:00";

                nrWorkingHoursFirstShift = 6;
                nrWorkingHoursSecondShift = 3;

                foreach (EmployeeBase employee in employeesWithPreferencesForAssigning)
                {
                    if (dataAccess.GetShiftPreferences(date.ToString("dd/MM/yyyy"), employee.EmployeeID) == firstShiftPeriod)
                    {
                        firstShift.Add(employee);
                    }
                    else if (dataAccess.GetShiftPreferences(date.ToString("dd/MM/yyyy"), employee.EmployeeID) == secondShiftPeriod)
                    {
                        secondShift.Add(employee);
                    }
                }
            }
            else if (date.DayOfWeek.ToString() == "Sunday")
            {
                firstShiftPeriod = "12:00-18:00";

                nrWorkingHoursFirstShift = 6;

                foreach (EmployeeBase employee in employeesWithPreferencesForAssigning)
                {
                    if (dataAccess.GetShiftPreferences(date.ToString("dd/MM/yyyy"), employee.EmployeeID) == firstShiftPeriod)
                    {
                        firstShift.Add(employee);
                    }
                }
            }
            else
            {
                firstShiftPeriod = "07:00-12:00";
                secondShiftPeriod = "12:00-17:00";
                thirdShiftPeriod = "17:00-22:00";

                nrWorkingHoursFirstShift = 5;
                nrWorkingHoursSecondShift = 5;
                nrWorkingHoursThirdShift = 5;

                foreach (EmployeeBase employee in employeesWithPreferencesForAssigning)
                {
                    if (dataAccess.GetShiftPreferences(date.ToString("dd/MM/yyyy"), employee.EmployeeID) == firstShiftPeriod)
                    {
                        firstShift.Add(employee);
                    }
                    else if (dataAccess.GetShiftPreferences(date.ToString("dd/MM/yyyy"), employee.EmployeeID) == secondShiftPeriod)
                    {
                        secondShift.Add(employee);
                    }
                    else if (dataAccess.GetShiftPreferences(date.ToString("dd/MM/yyyy"), employee.EmployeeID) == thirdShiftPeriod)
                    {
                        thirdShift.Add(employee);
                    }
                }
            }
        }



        //separate the employees in lists for shifts depending on their position (for first shift)

        //List<EmployeeBase> nrAdmins = new List<EmployeeBase>();
        //List<EmployeeBase> nrManagers = new List<EmployeeBase>();
        //List<EmployeeBase> nrDepotWorkers = new List<EmployeeBase>();
        //List<EmployeeBase> nrEmployees = new List<EmployeeBase>();



        //private void SeparateEmployeesByPosition(List<EmployeeBase> employeesForShift)
        //{
        //    foreach (EmployeeBase employee in employeesForShift)
        //    {
        //        if (dataAccess.GetPositionByID(employee.EmployeeID) == "ADMINISTRATOR" && !nrAdmins.Contains(employee))
        //        {
        //            nrAdmins.Add(employee);
        //        }
        //        else if (dataAccess.GetPositionByID(employee.EmployeeID) == "MANAGER" && !nrManagers.Contains(employee))
        //        {
        //            nrManagers.Add(employee);
        //        }
        //        else if (dataAccess.GetPositionByID(employee.EmployeeID) == "DEPOT" && !nrDepotWorkers.Contains(employee))
        //        {
        //            nrDepotWorkers.Add(employee);
        //        }
        //        else if (dataAccess.GetPositionByID(employee.EmployeeID) == "EMPLOYEE" && !nrEmployees.Contains(employee))
        //        {
        //            nrEmployees.Add(employee);
        //        }
        //    }
        //}

        private void GenerateScheduleForShiftByPosition(DateTime date, List<EmployeeBase> employeesForShift, List<EmployeeBase> Employees, string possition, int nrNeededEmployees, int nrWorkingHours, string department, string shift)
        {

            //checks the number of employees with preferences and the number of needed employees for the shift

            if (Employees.Count == nrNeededEmployees)
            {
                foreach (EmployeeBase employee in Employees)
                {
                    dataAccess.AssignEmployeeWithPreferencesToShift(employee.EmployeeID, date.ToString("dd/MM/yyyy"));
                    dataAccess.AddWorkingHoursToEmployee(employee.EmployeeID, nrWorkingHours);
                }
            }
            else if (Employees.Count > nrNeededEmployees)
            {
                for (int i = 0; i < nrNeededEmployees; i++)
                {
                    Random random = new Random();
                    int index = random.Next(Employees.Count);
                    dataAccess.AssignEmployeeWithPreferencesToShift(Employees[index].EmployeeID, date.ToString("dd/MM/yyyy"));
                    dataAccess.AddWorkingHoursToEmployee(Employees[index].EmployeeID, nrWorkingHours);
                    Employees.RemoveAt(index);
                }
            }
            else if (Employees.Count < nrNeededEmployees)
            {
                List<string> shifts = new List<string>();
                shifts.Add(firstShiftPeriod);
                shifts.Add(secondShiftPeriod);
                shifts.Add(thirdShiftPeriod);

                foreach (EmployeeBase employee in Employees)
                {
                    dataAccess.AssignEmployeeWithPreferencesToShift(employee.EmployeeID, date.ToString("dd/MM/yyyy"));
                    dataAccess.AddWorkingHoursToEmployee(employee.EmployeeID, nrWorkingHours);
                }

                List<EmployeeBase> allEmployees = dataAccess.GetEmployeesByPossitionsOrderedByWorkedHours(possition, department);

                for (int i = 0; i < nrNeededEmployees - Employees.Count; i++)
                {
                    //Random random = new Random();
                    //int index = random.Next(allEmployees.Count);
                    if (dataAccess.GetCurrentHoursByID(allEmployees[i].EmployeeID) < dataAccess.GetCurrentHoursByID(allEmployees[i + 1].EmployeeID))
                    {
                        dataAccess.AssignEmployeeToShift(allEmployees[i].EmployeeID, date.ToString("dd/MM/yyyy"), shift);
                        dataAccess.AddWorkingHoursToEmployee(allEmployees[i].EmployeeID, nrWorkingHours);
                    }
                    else
                    {
                        dataAccess.AssignEmployeeToShift(allEmployees[i + 1].EmployeeID, date.ToString("dd/MM/yyyy"), shift);
                        dataAccess.AddWorkingHoursToEmployee(allEmployees[i + 1].EmployeeID, nrWorkingHours);
                    }

                }
            }
        }

        public void GenerateScheduleForDay(DateTime date, string department, int nrAdminsPerShift, int nrManagersPerShift, int nrDepotWorkersPerShift, int nrEmployeesPerShift)
        {
            CheckEmployeesWhichCanWork(date, department);
            SeparateTheShiftPerDay(date);

            SeparateByPosition separate1 = new SeparateByPosition();
            separate1.SeparateEmployeesByPosition(firstShift);
            SeparateByPosition separate2 = new SeparateByPosition();
            separate2.SeparateEmployeesByPosition(secondShift);
            SeparateByPosition separate3 = new SeparateByPosition();
            separate3.SeparateEmployeesByPosition(thirdShift);



            //generate fisrt shifts
            GenerateScheduleForShiftByPosition(date, firstShift, separate1.GetAdmins(), "ADMINISTRATOR", nrAdminsPerShift, nrWorkingHoursFirstShift, department, firstShiftPeriod);
            GenerateScheduleForShiftByPosition(date, firstShift, separate1.GetManagers(), "MANAGER", nrManagersPerShift, nrWorkingHoursFirstShift, department, firstShiftPeriod);
            GenerateScheduleForShiftByPosition(date, firstShift, separate1.GetDepotWorkers(), "DEPOT", nrDepotWorkersPerShift, nrWorkingHoursFirstShift, department, firstShiftPeriod);
            GenerateScheduleForShiftByPosition(date, firstShift, separate1.GetEmployees(), "EMPLOYEE", nrEmployeesPerShift, nrWorkingHoursFirstShift, department, firstShiftPeriod);

            //generate second shifts
            GenerateScheduleForShiftByPosition(date, secondShift, separate2.GetAdmins(), "ADMINISTRATOR", nrAdminsPerShift, nrWorkingHoursSecondShift, department, secondShiftPeriod);
            GenerateScheduleForShiftByPosition(date, secondShift, separate2.GetManagers(), "MANAGER", nrManagersPerShift, nrWorkingHoursSecondShift, department, secondShiftPeriod);
            GenerateScheduleForShiftByPosition(date, secondShift, separate2.GetDepotWorkers(), "DEPOT", nrDepotWorkersPerShift, nrWorkingHoursSecondShift, department, secondShiftPeriod);
            GenerateScheduleForShiftByPosition(date, secondShift, separate2.GetEmployees(), "EMPLOYEE", nrEmployeesPerShift, nrWorkingHoursSecondShift, department, secondShiftPeriod);

            //generate third shifts
            GenerateScheduleForShiftByPosition(date, thirdShift, separate3.GetAdmins(), "ADMINISTRATOR", nrAdminsPerShift, nrWorkingHoursThirdShift, department, thirdShiftPeriod);
            GenerateScheduleForShiftByPosition(date, thirdShift, separate3.GetManagers(), "MANAGER", nrManagersPerShift, nrWorkingHoursThirdShift, department, thirdShiftPeriod);
            GenerateScheduleForShiftByPosition(date, thirdShift, separate3.GetDepotWorkers(), "DEPOT", nrDepotWorkersPerShift, nrWorkingHoursThirdShift, department, thirdShiftPeriod);
            GenerateScheduleForShiftByPosition(date, thirdShift, separate3.GetEmployees(), "EMPLOYEE", nrEmployeesPerShift, nrWorkingHoursThirdShift, department, thirdShiftPeriod);
        }

        public void GenerateScheduleForWeek(string department, int nrAdminsPerShift, int nrManagersPerShift, int nrDepotWorkersPerShift, int nrEmployeesPerShift)
        {
            DateTime today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                //GenerateScheduleForDay(today.Date, department, nrAdminsPerShift, nrManagersPerShift, nrDepotWorkersPerShift, nrEmployeesPerShift);
                for (int i = 0; i < 7; i++)
                {
                    GenerateScheduleForDay(today.AddDays(i).Date, department, nrAdminsPerShift, nrManagersPerShift, nrDepotWorkersPerShift, nrEmployeesPerShift);
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
                    GenerateScheduleForDay(nextMonday.AddDays(i).Date, department, nrAdminsPerShift, nrManagersPerShift, nrDepotWorkersPerShift, nrEmployeesPerShift);
                }
            }
        }

        private List<ScheduleBase> cancelledShiftsEmployeeID = new List<ScheduleBase>();
        private List<EmployeeBase> employeeWithLessHours;

        public void ReassigningCancelledShifts()
        {
            List<ScheduleBase> cancelledShifts = dataAccess.GetCancelledShifts();
            foreach (ScheduleBase sb in cancelledShifts)
            {
                cancelledShiftsEmployeeID.Add(sb);
            }

            foreach (ScheduleBase scheduleBase in cancelledShifts)
            {
                employeeWithLessHours = dataAccess.GetEmployeesForReassigning(dataAccess.GetPositionByID(scheduleBase.EmployeeId), dataAccess.GetEmployeeDepartamentByID(scheduleBase.EmployeeId));

                dataAccess.ReassignShift(employeeWithLessHours[0].EmployeeID, scheduleBase.Date, scheduleBase.Shift);

            }



        }
    }
}
