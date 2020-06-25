using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Media_Bazaar
{
    public class AssignShiftManagment
    {
        DataAccess db = new DataAccess();
        ScheduleBase schedule = new ScheduleBase();
        List<ScheduleBase> dbSchedules = null;
        DateTime shiftDate;

        public AssignShiftManagment(DateTime shiftDate)
        {
            this.shiftDate = shiftDate;
            dbSchedules = db.GetSchedulesByDate(this.shiftDate);
        }
        public void ShowComboBox(DateTime date, ComboBox weekDay, ComboBox saturday, ComboBox sunday)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                sunday.Visible = true;
                saturday.Visible = false;
                weekDay.Visible = false;
            }
            else
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    saturday.Visible = true;
                    sunday.Visible = false;
                    weekDay.Visible = false;
                }
                else
                {
                    weekDay.Visible = true;
                    sunday.Visible = false;
                    saturday.Visible = false;
                }
            }
        }

        public void UpdateList(ListBox lbShifts, string status)
        {
            lbShifts.Items.Clear();

            if (dbSchedules == null)
            {
                MessageBox.Show("No existing shifts for the selected day");
            }
            else
            {
                foreach (ScheduleBase sch in dbSchedules)
                {
                    string firstNameOfEmployee = db.GetFirstNameOfEmployeeById(sch.EmployeeId);

                    if (sch.Status == status)
                    {
                        if (sch.Attendance != null)
                        {
                            lbShifts.Items.Add($"{firstNameOfEmployee} - ID({sch.EmployeeId}):{sch.Shift} -> {sch.Attendance}");
                        }
                        else
                        {
                            lbShifts.Items.Add($"{firstNameOfEmployee} - ID({sch.EmployeeId}):{sch.Shift}");
                        }
                    }
                }
            }
        }

        public bool AssignWorkShift(int employeeId, string date, string shift)
        {
            List<EmployeeBase> empl = db.GetDBNotFiredEmployeeByID(employeeId);
            if (empl.Count != 0)
            {
                db.AddSchedule(employeeId, date, shift);
                return true;
            }
            return false;
        }

        public void AddCheckIn(string time, ListBox lbShifts)
        {
            string holder = "";
            string date = shiftDate.ToString("dd/MM/yyyy");
            string attendance = "";

            if (lbShifts.SelectedItem != null)
            {
                holder = lbShifts.SelectedItem.ToString();
                foreach (ScheduleBase sch in dbSchedules)
                {
                    if (holder.Contains(sch.EmployeeId.ToString()) && holder.Contains(sch.Shift))
                    {
                        db.AddCheckInForEmployeeByIdAndShift(sch.EmployeeId, time, sch.Shift, date);
                        holder = sch.Shift;
                        break;
                    }
                }
            }

            holder = holder.Substring(0, 5);

            DateTime time1 = DateTime.Parse(time);
            DateTime time2 = DateTime.Parse(holder);

            if (time1.TimeOfDay > time2.TimeOfDay)
            {
                attendance = "LATE";

                if (lbShifts.SelectedItem != null)
                {
                    holder = lbShifts.SelectedItem.ToString();
                    foreach (ScheduleBase sch in dbSchedules)
                    {
                        if (holder.Contains(sch.EmployeeId.ToString()) && holder.Contains(sch.Shift))
                        {
                            db.AddAttendanceForEmployeeByIdAndShift(sch.EmployeeId, attendance, sch.Shift, date);
                            break;
                        }
                    }
                }
            }

            else if (time1.TimeOfDay == time2.TimeOfDay)
            {
                attendance = "PRESENT";

                if (lbShifts.SelectedItem != null)
                {
                    holder = lbShifts.SelectedItem.ToString();
                    foreach (ScheduleBase sch in dbSchedules)
                    {
                        if (holder.Contains(sch.EmployeeId.ToString()) && holder.Contains(sch.Shift))
                        {
                            db.AddAttendanceForEmployeeByIdAndShift(sch.EmployeeId, attendance, sch.Shift, date);
                            break;
                        }
                    }
                }
            }

            else if (time1.TimeOfDay < time2.TimeOfDay)
            {
                attendance = "PRESENT";

                if (lbShifts.SelectedItem != null)
                {
                    holder = lbShifts.SelectedItem.ToString();
                    foreach (ScheduleBase sch in dbSchedules)
                    {
                        if (holder.Contains(sch.EmployeeId.ToString()) && holder.Contains(sch.Shift))
                        {
                            db.AddAttendanceForEmployeeByIdAndShift(sch.EmployeeId, attendance, sch.Shift, date);
                            break;
                        }
                    }
                }
            }
        }

        public void AddCheckOut(string time, ListBox lbShifts)
        {
            string holder = "";
            string date = shiftDate.ToString("dd/MM/yyyy");

            if (lbShifts.SelectedItem != null)
            {
                holder = lbShifts.SelectedItem.ToString();
                foreach (ScheduleBase sch in dbSchedules)
                {
                    if (holder.Contains(sch.EmployeeId.ToString()) && holder.Contains(sch.Shift))
                    {
                        db.AddCheckOutForEmployeeByIdAndShift(sch.EmployeeId, time, sch.Shift, date);
                        break;
                    }
                }
            }
        }

        public void AddAttendance(string attendance, ListBox lbShifts)
        {
            string holder = "";
            string date = shiftDate.ToString("dd/MM/yyyy");

            if (lbShifts.SelectedItem != null)
            {
                holder = lbShifts.SelectedItem.ToString();
                foreach (ScheduleBase sch in dbSchedules)
                {
                    if (holder.Contains(sch.EmployeeId.ToString()) && holder.Contains(sch.Shift))
                    {
                        db.AddAttendanceForEmployeeByIdAndShift(sch.EmployeeId, attendance, sch.Shift, date);
                        break;
                    }
                }
            }
        }

        public bool DeleteAttendance(ListBox lbShifts, string status)
        {
            string holder = "";
            string date = shiftDate.ToString("dd/MM/yyyy");

            if (lbShifts.SelectedItem != null)
            {
                holder = lbShifts.SelectedItem.ToString();
                foreach (ScheduleBase sch in dbSchedules)
                {
                    if (holder.Contains(sch.EmployeeId.ToString()) && holder.Contains(sch.Shift))
                    {
                        DialogResult dialogResult = MessageBox.Show($"Are you sure that you want to delete this shift? ID({sch.EmployeeId}): {sch.Shift}", "Warning!", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            db.DeleteAttendanceByIdAndShift(sch.EmployeeId, sch.Shift, date, status);
                            MessageBox.Show("Shift has been successfully removed!");
                            return true;
                        }
                        else //if(dialogResult == DialogResult.No)
                        {
                            //do nothing 
                        }
                        break;
                    }
                }
            }
            return false;
        }

        public bool AssignSelectedShift(ListBox lbShiftPreferences)
        {
            string holder = "";
            string date = shiftDate.ToString("dd/MM/yyyy");

            if (lbShiftPreferences.SelectedItem != null)
            {
                holder = lbShiftPreferences.SelectedItem.ToString();
                foreach (ScheduleBase sch in dbSchedules)
                {
                    if (holder.Contains(sch.EmployeeId.ToString()) && holder.Contains(sch.Shift))
                    {
                        DialogResult dialogResult = MessageBox.Show($"Are you sure that you want to assign this shift? ID({sch.EmployeeId}): {sch.Shift}", "Warning!", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            db.AssignShift(sch.EmployeeId, sch.Shift, date);
                            MessageBox.Show("Shift has been successfully assigned!");
                            return true;
                        }
                        else //if(dialogResult == DialogResult.No)
                        {
                            //do nothing 
                        }
                        break;
                    }
                }
            }
            return false;
        }

    }
}
