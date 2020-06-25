using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Media_Bazaar.Classes;
using Media_Bazaar.LogicLayer.Products;
using MySql.Data.MySqlClient;

namespace Media_Bazaar
{
    public class DataAccess
    {
        //METHODS FOR EMPLOYEES:      

        public List<EmployeeBase> GetNotFiredEmployeesByLastName(string lastName)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<EmployeeBase>($"SELECT * FROM Employee WHERE LastName='{lastName}' AND ReasonsForRelease IS NULL").ToList();
                return output;
            }
        }
        public List<EmployeeBase> GetNotFiredEmployeesByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<EmployeeBase>($"SELECT * FROM Employee WHERE EmployeeID='{id}' AND ReasonsForRelease IS NULL").ToList();
                return output;
            }
        }

        //get all the info of the employee via employee id.
        public List<EmployeeBase> GetDBNotFiredEmployeeByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<EmployeeBase>($"SELECT * FROM Employee WHERE EmployeeID='{id}' AND ReasonsForRelease IS NULL").ToList();
                return output;
            }
        }
        //get all info of the employee via last name.
        public List<EmployeeBase> GetDBEmployeesByLastName(string lastName)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<EmployeeBase>($"SELECT * FROM Employee WHERE LastName='{lastName}'").ToList();
                return output;
            }
        }

        public void InsertEmployee(string fName, string lName, string dateOfBirth, string email, string phoneNr, string nationality, string pos, int minHours, int maxHours, double wage, string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"INSERT INTO Employee(FirstName, LastName, DateOfBirth, Email, PhoneNumber, Nationality, Position,MinHrsPerWeek,MaxHrsPerWeek,WagePerHour, Username, Password) VALUES ('{ fName }', '{lName}' , '{dateOfBirth}' , '{email}' , '{phoneNr}' , '{nationality}','{pos}' , '{minHours}' ,'{maxHours}','{wage}', '{username}' , '{password}');");
            }
        }


        //get the employee id via name.
        public int GetIdOfEmployeeByName(string fName, string lName)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<int>($"SELECT EmployeeID FROM Employee WHERE FirstName ='{fName}' AND LastName ='{lName}';");
            }
        }


        //get first name of employee via employee id.
        public string GetFirstNameOfEmployeeById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT FirstName FROM Employee WHERE EmployeeID = '{id}';");
            }
        }

        //Get All employees
        public List<EmployeeBase> GetAllEmployees()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<EmployeeBase>($"SELECT * FROM Employee").ToList();
                return output;
            }
        }


        //update and set the info's table in data base where the employee is fired by id.
        public void FireEmployeeByID(string reasons, int employeeID)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE Employee SET ReasonsForRelease='{reasons}' WHERE EmployeeID='{employeeID}'; ");
            }
        }

        //not used till now
        //public List<DBEmployee> GetFiredEmployees()
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
        //    {
        //        var output = connection.Query<DBEmployee>($"SELECT * FROM Employee WHERE ReasonsForRelease IS NOT NULL;").ToList();
        //        return output;
        //    }
        //}

        //get all the info of not fired employees

        public List<EmployeeBase> GetNotFiredEmployees()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<EmployeeBase>($"SELECT * FROM Employee WHERE ReasonsForRelease IS NULL;").ToList();
                //return connection.Query<EmployeeBase>($"SELECT * FROM Employee WHERE ReasonsForRelease IS NULL;").ToList();
                return output;
            }
        }

        public string GetDepotID()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT EmployeeID FROM Employee WHERE Position ='Depot' ;");
            }
        }

        //METHODS FOR DEPARTAMENTS

        //add new department in the deartment table in data base.
        public void InsertDepartament(string departamentName, int minNumOfEmployees, int maxNumOfEmployees)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"INSERT INTO Departament(DepartamentName, MinNumOfEmployees, MaxNumOfEmployees) VALUES ('{ departamentName }', '{minNumOfEmployees}' , '{maxNumOfEmployees}');");

            }
        }

        //get all the departments that are in the data base.
        public List<DepartmentModel> GetAllDepartaments()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<DepartmentModel>($"SELECT * FROM Departament WHERE status IS NULL").ToList();
                return output;
            }
        }

        //update employee table when information are added to the department column.
        public void AssignEmployeeToDepartament(int employeeID, string departament)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE Employee SET Departament ='{departament}' WHERE EmployeeID ='{employeeID}'; ");
            }

        }

        //get stock id by the given stock id.
        public string GetDBStockIDById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT RequestID FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //get stock name by the given stock id.
        public string GetDBStockNameById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT ProductName FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //get stock type by the given stock id.
        public string GetDBStockTypeById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT Category FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //get department by the given stock id.
        public string GetDBDepartmentByStockId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT Departament FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //get brand by the given stock id.
        public string GetDBBrandByStockId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT Brand FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //get stock quantity id by the given stock id.
        public string GetDBStockQuantityById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT Quantity FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //get stock quantity by the given stock name.
        public int GetStockQuantityByName(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<int>($"SELECT Quantity FROM RestockRequest WHERE ProductName='{name}';");
            }
        }

        //get stock order date id by the given stock id.
        public string GetDBStockOrderDateById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT DateOfOrder FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //get stock deliver date id by the given stock id.
        public string GetDBStockDeliverDateById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT DateOfDelivery FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //get stock status by the given stock id.
        public string GetDBStockStatusById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT AdminConfirmation FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //get employee id by the given stock id.
        public string GetDBEmployeeIdByStockId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT EmployeeID FROM RestockRequest WHERE RequestID ='{id}';");
            }
        }

        //METHODS FOR RESTOCK

        // get info about all requested stock.
        public List<RestockRequestBase> GetAllRequests()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<RestockRequestBase>($"SELECT * FROM RestockRequest").ToList();
                return output;
            }
        }

        // get info about the name of the requested stock.
        /*public string GetRequestsName()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT ProductName FROM RestockRequest");
            }
        }*/

        // get info about the name of the requested stock.
        public List<RestockRequestBase> GetRequestsName()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<RestockRequestBase>($"SELECT ProductName FROM RestockRequest").ToList();
                return output;
            }
        }

        //not used till now
        //public List<DBRestockRequest> GetAllIncomingStockRequests(DateTime dt)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
        //    {
        //        var output = connection.Query<DBRestockRequest>($"SELECT * FROM RestockRequest WHERE DateOfDelivery>='{dt}' AND AdminConfirmation ='CONFIRMED'").ToList();
        //        return output;
        //    }
        //}

        // add request to Restock request table in data base.
        public void InsertRequest(int idE, string name, string category, string brand, string department, int quantity, string orderDate, string orderDeliver)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"INSERT INTO RestockRequest(EmployeeID, ProductName, Category, Brand, Departament, Quantity, DateOfOrder, DateOfDelivery) VALUES ('{idE}' , '{name}' , '{category}', '{brand}' , '{department}', '{quantity}','{orderDate}', '{orderDeliver}');");
            }
        }

        public void UpdataData(int id, int quantity)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE RestockRequest SET Quantity='{quantity}' WHERE RequestID='{id}'; ");
            }
        }

        //add request to restock request table in data base. ((Will be used in the next block))
        //public void InsertRequestForAnExistingstock(int idS,int idE,string name, string type, string department,int quantity, string orderDate, string orderDeliver)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
        //    {
        //        connection.Execute($"INSERT INTO RestockRequest(RequestID, EmployeeID, ProductName,Category, Departament, Quantity, DateOfOrder, DateOfDelivery) VALUES ('{idS}' , '{idE}' , '{name}', '{type}', '{department}' ,'{quantity}','{orderDate}', '{orderDeliver}');");
        //    }
        //}

        // get all confirmed requests from data base.
        public List<RestockRequestBase> GetAllConfirmedRestock()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<RestockRequestBase>($"SELECT * FROM RestockRequest WHERE AdminConfirmation ='CONFIRMED'").ToList();
                return output;
            }
        }

        // Get all rejected requests.
        public List<RestockRequestBase> GetAllRejectedRestock()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<RestockRequestBase>($"SELECT * FROM RestockRequest WHERE AdminConfirmation='REJECTED'").ToList();
                return output;
            }
        }

        //update restock request when the AdminConfirmation column is modified "CONFIRMED".
        public void ConfirmRequest(int requestID)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE RestockRequest SET AdminConfirmation='CONFIRMED' WHERE RequestID='{requestID}'; ");
            }
        }

        //update restock request when the AdminConfirmation column is modified "REJECTED".
        public void RejectRequest(int requestID)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE RestockRequest SET AdminConfirmation='REJECTED' WHERE RequestID ='{requestID}'; ");
            }
        }

        //get all available stocks where the quantity is not null
        public List<RestockRequestBase> GetAllAvailableStocks()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<RestockRequestBase>($"SELECT * FROM RestockRequest WHERE Quantity IS NOT NULL").ToList();
                return output;
            }
        }


        //METHODS FOR SCHEDULE--------------

        //get all the schedule from the data base.
        public List<ScheduleBase> GetAllSchedules()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<ScheduleBase>($"SELECT * FROM Schedule").ToList();
                return output;
            }
        }
        public List<ScheduleBase> GetSchedulesByDate(DateTime shiftDate)
        {
            string date = shiftDate.ToString("dd/MM/yyyy");
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<ScheduleBase>($"SELECT * FROM Schedule WHERE Date='{date}'").ToList();
                return output;
            }
        }

        public List<ScheduleBase> GetSchedulesByEmplId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<ScheduleBase>($"SELECT * FROM schedule WHERE EmployeeID='{id}' AND NOT Status='Cancelled'").ToList();
                return output;
            }
        }
        //Adding the shift to the database
        public void AddSchedule(int employeeID, string date, string shift)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {

                int id = connection.ExecuteScalar<int>($"SELECT e.EmployeeID FROM Employee AS e WHERE e.EmployeeID='{employeeID}'");
                connection.Execute($"INSERT INTO schedule (EmployeeID, Date, Shift)  VALUES( '{id}', '{date}', '{shift}')");
            }
        }
        //Add check in time to the database
        public void AddCheckInForEmployeeByIdAndShift(int id, string time, string shift, string date)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE schedule SET CheckIn='{time}' WHERE EmployeeID='{id}' AND Shift='{shift}' AND Date='{date}';");
            }
        }
        //Add check out time to the database
        public void AddCheckOutForEmployeeByIdAndShift(int id, string time, string shift, string date)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE schedule SET CheckOut='{time}' WHERE EmployeeID='{id}' AND Shift='{shift}' AND Date='{date}';");
            }
        }
        //get employee attendance by employee id, shift and date.
        public void AddAttendanceForEmployeeByIdAndShift(int id, string attendance, string shift, string date)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE schedule SET Attendance='{attendance}' WHERE EmployeeID='{id}' AND Shift='{shift}' AND Date='{date}';");
            }
        }

        public void DeleteAttendanceByIdAndShift(int emplId, string shift, string date, string status)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"DELETE FROM schedule WHERE EmployeeID='{emplId}' AND Shift='{shift}' AND Date ='{date}' AND Status='{status}';");
            }
        }
        public void AssignShift(int id, string shift, string date)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE schedule SET Status='Assigned' WHERE EmployeeID='{id}' AND Shift='{shift}' AND Date='{date}';");
            }
        }





        // methods to return int in order to use it in the statistics

        //return number of present employees.
        public int GetNumOfPresentById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<int>($"SELECT COUNT(*) FROM Schedule WHERE Attendance='PRESENT' AND EmployeeID='{id}';");
            }
        }
        //return number of absent employees.
        public int GetNumOfAbsentById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<int>($"SELECT COUNT(*) FROM Schedule WHERE Attendance='ABSENT' AND EmployeeID ='{id}';");
            }
        }

        //return number of late employees.
        public int GetNumOfLateById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<int>($"SELECT COUNT(*) FROM Schedule WHERE Attendance='LATE' AND EmployeeID='{id}';");
            }
        }


        //EMPLOYEE EMAILS
        public List<EmailModel> GetEmails()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<EmailModel>($"SELECT * FROM email").ToList();
                return output;
            }
        }

        public void EmailStatusRead(int emailId)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE email SET Status='Read' WHERE EmailID='{emailId}'; ");
            }
        }

        public void DeleteEmailById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"DELETE FROM email WHERE EmailID='{id}';");
            }
        }

        //LOGIN METHODS

        public DataTable LoginAdministrator(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                MySqlCommand cm = new MySqlCommand($"SELECT Username, Password FROM employee WHERE Username='{username}' AND Password='{password}' AND Position='ADMINISTRATOR';", connection);
                MySqlDataAdapter sda = new MySqlDataAdapter(cm);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        public DataTable LoginManager(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                MySqlCommand cm = new MySqlCommand($"SELECT Username, Password FROM employee WHERE Username='{username}' AND Password='{password}' AND Position='MANAGER';", connection);
                MySqlDataAdapter sda = new MySqlDataAdapter(cm);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        public DataTable LoginDepotWorker(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                MySqlCommand cm = new MySqlCommand($"SELECT Username, Password FROM employee WHERE Username='{username}' AND Password='{password}' AND Position='DEPOT';", connection);
                MySqlDataAdapter sda = new MySqlDataAdapter(cm);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        //products

        //public List<Product> GetFromDBProductInfo(string brand)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
        //    {
        //        var output = /*connection.Query<DBProduct>($"SELECT * FROM product WHERE Brand='{brand}'").ToList();*/
        //            connection.Query<Product>($"SELECT p.id, p.product_name, p.Category, p.Brand, r.Departament, r.Quantity " +
        //            $"FROM restockRequest r INNER JOIN product p ON p.id = r.product_id WHERE p.Brand = '{brand}'" +
        //            $"AND r.AdminConfirmation = 'CONFIRMED' ").ToList();
        //        return output;
        //    }
        //}

        public List<RestockRequest> GetProductInfo(string brand)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<RestockRequest>($"SELECT * FROM restockrequest WHERE Brand = '{brand}'" +
                    $"AND AdminConfirmation = 'CONFIRMED' ").ToList();
                return output;
            }
        }

        //GetProduct data to export them
        public List<RestockRequest> GetProductData()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<RestockRequest>($"SELECT * FROM restockrequest").ToList();
                return output;
            }
        }

        //searching for products by Brand
        public List<RestockRequest> GetProductInfoByBrand(string brand)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<RestockRequest>($"SELECT * FROM restockrequest WHERE Brand = '{brand}'" +
                    $"AND AdminConfirmation = 'CONFIRMED' ").ToList();
                return output;
            }
        }

        //searching for products by department
        public List<RestockRequest> GetProductInfoByDepartment(string dep)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<RestockRequest>($"SELECT * FROM restockrequest WHERE Departament = '{dep}'" +
                    $"AND AdminConfirmation = 'CONFIRMED' ").ToList();
                return output;
            }
        }

        // Update product Data
        public void UpdateProduct(int id, string name, string brand, string department, string category, int quantity)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"Update restockrequest SET ProductName='{name}', Brand='{brand}'," +
                    $"Quantity='{quantity}', Departament='{department}', Category='{category}' WHERE RequestID='{id}'; ");
            }
        }

        // delete product from stock
        public void DeleteProduct(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"DELETE FROM restockrequest WHERE RequestID='{id}';");
            }
        }

        // update employee info
        public void UpdateEmployee(int id, string dateOfBirth, string nationality, string email, string phoneNumber, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"Update employee SET DateOfBirth='{dateOfBirth}', Email='{email}'," +
                    $"PhoneNumber='{phoneNumber}', Nationality='{nationality}', Password='{password}' WHERE EmployeeID='{id}'; ");
            }
        }

        //for departaments searching
        public List<DepartmentModel> GetDepInfo(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<DepartmentModel>($"SELECT * FROM departament WHERE DepartamentName = '{name}'" +
                    $"AND status IS NULL ").ToList();
                return output;
            }
        }

        //remove department by name
        public void RemoveDepartment(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"Update departament SET status='Removed' WHERE DepartamentName='{name}'; ");
                connection.Execute($"Update employee SET ReasonsForRelease='DepartmentIsRemoved' WHERE Departament='{name}' " +
                    $"AND ReasonsForRelease IS NULL; ");
                //connection.Execute($"DELETE FROM departament WHERE DepartamentName='{name}'; ");
            }
        }

        //ALGORITHM METHODS


        //Return all preferences for a certain date
        public List<EmployeeBase> GetEmployeesByDepartment(string department)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                //var output = connection.Query<EmployeeBase>($"SELECT * FROM schedule WHERE EmployeeID IN (SELECT EmployeeID FROM employee WHERE Departament='{department}') AND Date='{date}' AND Status='Selected';").ToList();
                var output = connection.Query<EmployeeBase>($"SELECT * FROM employee WHERE Departament='{department}';").ToList();
                return output;
            }
        }
        public List<EmployeeBase> GetEmployeesPreferencesForDayByDepartment(string date,string department)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                //var output = connection.Query<EmployeeBase>($"SELECT * FROM schedule WHERE EmployeeID IN (SELECT EmployeeID FROM employee WHERE Departament='{department}') AND Date='{date}' AND Status='Selected';").ToList();
                var output = connection.Query<EmployeeBase>($"SELECT * FROM employee e INNER JOIN schedule s ON e.EmployeeID = s.EmployeeID WHERE e.Departament='{department}' AND s.Date='{date}' AND s.Status='Selected';").ToList();
                return output;
            }
        }
        public string GetPositionByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT Position FROM Employee WHERE EmployeeID='{id}'");
            }
        }

        public int GetMaxHoursByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<int>($"SELECT MaxHrsPerWeek FROM Employee WHERE EmployeeID='{id}'");
            }
        }

        public int GetMinHoursByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<int>($"SELECT MinHrsPerWeek FROM Employee WHERE EmployeeID='{id}'");
            }
        }

        public int GetCurrentHoursByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<int>($"SELECT CurrentHoursForWeek FROM Employee WHERE EmployeeID='{id}'");
            }
        }

        public string GetShiftPreferences(string date,int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT Shift FROM schedule WHERE Date='{date}' AND Status='Selected' AND EmployeeID='{id}';");
            }
        }

        public void AddWorkingHoursToEmployee(int id,int hours)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE Employee SET CurrentHoursForWeek=CurrentHoursForWeek+{hours} WHERE EmployeeID='{id}';");
            }
        }

        public void AssignEmployeeWithPreferencesToShift(int id, string date)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE schedule SET Status='Assigned' WHERE EmployeeID='{id}' AND Date='{date}' ;");
            }
        }

        public void AssignEmployeeToShift(int id,string date,string shift)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"INSERT INTO schedule (EmployeeID, Date, Shift, Status)  VALUES( '{id}', '{date}', '{shift}','Assigned')");
            }
        }

        public List<EmployeeBase> GetEmployeesByPossitionsOrderedByWorkedHours(string possition, string department)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<EmployeeBase>($"SELECT * FROM employee WHERE Position='{possition}' AND Departament='{department}' AND ReasonsForRelease IS NULL ORDER BY CurrentHoursForWeek;").ToList();
                return output;
            }
        }


        public List<ScheduleBase> GetCancelledShifts()
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<ScheduleBase>($"SELECT * FROM schedule WHERE Status='Cancelled';").ToList();
                return output;
            }
        }

        public string GetEmployeeDepartamentByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                return connection.ExecuteScalar<string>($"SELECT Departament FROM `employee` WHERE EmployeeID='{id}'");
            }
        }

        public void ReassignShift(int id,string date,string shift)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                connection.Execute($"UPDATE schedule SET Status='Assigned', EmployeeID='{id}' WHERE Date='{date}' AND Shift='{shift}';");
            }
        }


        public List<EmployeeBase> GetEmployeesForReassigning(string possition, string department)
        {
            using (MySqlConnection connection = new MySqlConnection(Helper.CnnVal("DB")))
            {
                var output = connection.Query<EmployeeBase>($" SELECT `EmployeeID` FROM `employee` WHERE Departament = '{department}' AND Position = '{possition}' ORDER BY CurrentHoursForWeek").ToList();
                return output;
            }
        }


       
    }
}
