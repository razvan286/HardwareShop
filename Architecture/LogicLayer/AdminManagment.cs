using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Media_Bazaar
{
    public class AdminManagment
    {
        private DataAccess dataAccess = new DataAccess();
        private SendEmail sendEmail = new SendEmail();
        private EmployeeBase employeeModel;
        private ScheduleGenerator scheduleGenerator = new ScheduleGenerator();
        Calendar calendar = new Calendar();
        ScheduleBase schedule = new ScheduleBase();

        private string employeeCredentials = "";

        public bool RemoveFDepartment(string name) //remove department
        {
            dataAccess.RemoveDepartment(name);
            return true;
        }

        public string CreateNewProfile(string fName, string lName, string dateOfBirth, string email, string phoneNr, string nationality, string pos, string minHours, string maxHours, string wage)
        {
            if (!String.IsNullOrEmpty(fName) && !String.IsNullOrEmpty(lName) && !String.IsNullOrEmpty(dateOfBirth) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(phoneNr) && !String.IsNullOrEmpty(nationality) && !String.IsNullOrEmpty(minHours) && !String.IsNullOrEmpty(maxHours) && !String.IsNullOrEmpty(wage))
            {
                if (pos == "ADMINISTRATOR")
                {
                    employeeModel = new EmployeeBase { FirstName = fName, LastName = lName, DateOfBirth = dateOfBirth, Email = email, PhoneNumber = phoneNr, Nationality = nationality, Position = pos, MinHoursPerWeek = Convert.ToInt32(minHours), MaxHoursPerWeek = Convert.ToInt32(maxHours), WagePerHour = Convert.ToDouble(wage), CurrentHours = 0 };
                }
                else if (pos == "MANAGER")
                {
                    employeeModel = new EmployeeBase { FirstName = fName, LastName = lName, DateOfBirth = dateOfBirth, Email = email, PhoneNumber = phoneNr, Nationality = nationality, Position = pos, MinHoursPerWeek = Convert.ToInt32(minHours), MaxHoursPerWeek = Convert.ToInt32(maxHours), WagePerHour = Convert.ToDouble(wage), CurrentHours = 0 };
                }
                else if (pos == "DEPOT")
                {
                    employeeModel = new EmployeeBase { FirstName = fName, LastName = lName, DateOfBirth = dateOfBirth, Email = email, PhoneNumber = phoneNr, Nationality = nationality, Position = pos, MinHoursPerWeek = Convert.ToInt32(minHours), MaxHoursPerWeek = Convert.ToInt32(maxHours), WagePerHour = Convert.ToDouble(wage), CurrentHours = 0 };
                }
                else if (pos == "EMPLOYEE")
                {
                    employeeModel = new EmployeeBase { FirstName = fName, LastName = lName, DateOfBirth = dateOfBirth, Email = email, PhoneNumber = phoneNr, Nationality = nationality, Position = pos, MinHoursPerWeek = Convert.ToInt32(minHours), MaxHoursPerWeek = Convert.ToInt32(maxHours), WagePerHour = Convert.ToDouble(wage), CurrentHours = 0 };
                }

                dataAccess.InsertEmployee(fName, lName, dateOfBirth, email, phoneNr, nationality, employeeModel.Position, Convert.ToInt32(minHours), Convert.ToInt32(maxHours), Convert.ToDouble(wage), employeeModel.Username, employeeModel.Password);

                int employeeID = dataAccess.GetIdOfEmployeeByName(fName, lName);

                employeeCredentials = $"{employeeID.ToString()} \r\n {employeeModel.Username} \r\n {employeeModel.Password}";

                sendEmail.Send(email, employeeModel.Username, employeeModel.Password);
                return employeeCredentials;
            }
            else
            {
                return null;
            }
        }

        public string GetCredentials()
        {
            return employeeCredentials;
        }

        public bool FireEmployee(string ExtraInformationForFire, int id, string emplID)
        {
            //if (String.IsNullOrEmpty(ExtraInformationForFire))
            //{
            /// return false;
            // }
            // else
            // {
            if (emplID.Contains($"ID:{id}"))
            {
                dataAccess.FireEmployeeByID(ExtraInformationForFire, id);
                return true;
            }
            return false;
            // }
        }

        public bool AssignEmployeeToDepartment(int id, string departament, string emplID)
        {
            if (emplID.Contains($"ID:{id}"))
            {
                dataAccess.AssignEmployeeToDepartament(id, departament);
                return true;
            }
            return false;
        }

        public bool ConfirmRequest(int id, string req)
        {
            if (req.Contains($"ID:{id}"))
            {
                dataAccess.ConfirmRequest(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RejectRequest(int id, string req)
        {
            if (req.Contains($"ID:{id}"))
            {
                dataAccess.RejectRequest(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CreateDepartment(string DepartmentName, string minNr, string MaxNr)
        {
            if (!String.IsNullOrEmpty(DepartmentName) && !String.IsNullOrEmpty(minNr) && !String.IsNullOrEmpty(MaxNr))
            {
                dataAccess.InsertDepartament(DepartmentName, Convert.ToInt32(minNr), Convert.ToInt32(MaxNr));
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GenerateSchedule(string department, int nrAdminsPerShift, int nrManagersPerShift, int nrDepotWorkersPerShift, int nrEmployeesPerShift)
        {
            scheduleGenerator.GenerateScheduleForWeek(department, nrAdminsPerShift, nrManagersPerShift, nrDepotWorkersPerShift, nrEmployeesPerShift);
        }


        // ------ CALENDAR ---- //
        public void CreateCalendar(FlowLayoutPanel flDays, Label label)
        {
            schedule.GetAllSchedules();
            calendar.GenerateDayPanel(42, flDays);
            calendar.DisplayCurrentDate(schedule.allSchedules, label);
        }

        public void CalendarPreviousMonth(Label label)
        {
            calendar.PrevMonth(schedule.allSchedules, label);
        }
        public void CalendarToday(Label label)
        {
            calendar.Today(schedule.allSchedules, label);
        }
        public void CalendarNextMonth(Label label)
        {
            calendar.NextMonth(schedule.allSchedules, label);
        }
        //----------------------------------------


        public List<EmployeeBase> GetNotFiredEmployees()
        {
            return dataAccess.GetNotFiredEmployees();
        }

        public List<DepartmentModel> GetAllDepartaments()
        {
            return dataAccess.GetAllDepartaments();
        }

        public List<RestockRequestBase> GetAllRestockRequests()
        {
            return dataAccess.GetAllRequests();
        }

        public List<EmailModel> GetEmails()
        {
            return dataAccess.GetEmails();
        }

        public string GetFirstNameOfEmployeeById(int id)
        {
            return dataAccess.GetFirstNameOfEmployeeById(id);
        }

        public void EmailStatusRead(int emailid)
        {
            dataAccess.EmailStatusRead(emailid);
        }

        public void DeleteEmailById(int id)
        {
            dataAccess.DeleteEmailById(id);
        }

        public List<ScheduleBase> GetCancelledShifts()
        {
            return dataAccess.GetCancelledShifts();
        }

        public void Reasign()
        {
            scheduleGenerator.ReassigningCancelledShifts();
        }
    }
}