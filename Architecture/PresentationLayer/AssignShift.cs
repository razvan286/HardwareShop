using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Media_Bazaar.Classes;

namespace Media_Bazaar
{
    public partial class AssignShift : Form
    {
        DateTime shiftDate;
        AssignShiftManagment assignManager;

        public AssignShift(DateTime date, MainAdmin main)
        {
            InitializeComponent();
            this.shiftDate = date;
            this.Text = $"Assign shift on date: {tbDate.Text}";
            tbDate.Text = $"{date.Day}/{date.Month}/{date.Year}, {date.DayOfWeek}";

            assignManager = new AssignShiftManagment(shiftDate);
            assignManager.ShowComboBox(date, cmbBxWorkShiftWeekDay, cmbBxWorkShiftSaturday, cmbBxWorkShiftSunday);
        }
        private void AssignShift_Load(object sender, EventArgs e)
        {
            UpdateList();
            UpdatePreferencesList();
            UpdateAbsenceList();
        }
        private void UpdateList()
        {
            assignManager.UpdateList(lbShifts, "Assigned");
        }
        private void UpdatePreferencesList()
        {
            assignManager.UpdateList(lbShiftPreferences, "Selected");
        }
        private void UpdateAbsenceList()
        {
            assignManager.UpdateList(lbxAbsenceRecods, "Cancelled");
        }
        private void btnAssignWorkShift_Click(object sender, EventArgs e)
        {
            int employeeId = -1;
            string date = "";
            string shift = "";
            if (tbEmployeeIdAssignShift.Text != "" && (cmbBxWorkShiftSaturday.SelectedItem != null || cmbBxWorkShiftSunday.SelectedItem != null || cmbBxWorkShiftWeekDay.SelectedItem != null))
            {
                employeeId = Convert.ToInt32(tbEmployeeIdAssignShift.Text);
                date = shiftDate.ToString("dd/MM/yyyy");
                if (shiftDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    shift = "12:00-18:00";
                }
                else
                {
                    if (shiftDate.DayOfWeek == DayOfWeek.Saturday && cmbBxWorkShiftSaturday.SelectedItem.ToString() == "Morning -> 09:00-15:00")
                    {
                        shift = "09:00-15:00";
                    }
                    else
                    {
                        if (shiftDate.DayOfWeek == DayOfWeek.Saturday && cmbBxWorkShiftSaturday.SelectedItem.ToString() == "Afternoon -> 15:00-18:00")
                        {
                            shift = "15:00-18:00";
                        }
                        else
                        {
                            if (cmbBxWorkShiftWeekDay.SelectedItem.ToString() == "Morning -> 07:00-12:00")
                            {
                                shift = "07:00-12:00";
                            }
                            else
                            {
                                if (cmbBxWorkShiftWeekDay.SelectedItem.ToString() == "Afternoon -> 12:00-17:00")
                                {
                                    shift = "12:00-17:00";
                                }
                                else
                                {
                                    if (cmbBxWorkShiftWeekDay.SelectedItem.ToString() == "Evening -> 17:00-22:00")
                                    {
                                        shift = "17:00-22:00";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (employeeId != -1 && date != "" && shift != "")
            {

                if (assignManager.AssignWorkShift(employeeId, date, shift) == false)
                {
                    MessageBox.Show("No employee found with the specified ID. He may be fired.");
                }
                else
                {
                    assignManager = new AssignShiftManagment(shiftDate);
                    UpdateList();
                }
            }
        }

        private void AddAttendance(string attendance)
        {
            assignManager.AddAttendance(attendance, lbShifts);
            assignManager = new AssignShiftManagment(shiftDate);
            UpdateList();
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            assignManager = new AssignShiftManagment(shiftDate); //not sure if I need to use this line
            assignManager.AddCheckIn(time, lbShifts);

            MessageBox.Show("Checked in at ", time);
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            assignManager = new AssignShiftManagment(shiftDate); //not sure if I need to use this line
            assignManager.AddCheckOut(time, lbShifts);

            MessageBox.Show("Checked out at ", time);
        }

        /*private void btnPresent_Click(object sender, EventArgs e)
        {
            string attendance = "PRESENT";
            AddAttendance(attendance);
        }

        private void btnLate_Click(object sender, EventArgs e)
        {
            string attendance = "LATE";
            AddAttendance(attendance);
        }*/

        private void btnAbsent_Click(object sender, EventArgs e)
        {
            string attendance = "ABSENT";
            AddAttendance(attendance);
        }

        private void lbItem_DoubleClick(object sender, EventArgs e)
        {
            string status = "Assigned";
            if (assignManager.DeleteAttendance(lbShifts, status) == true)
            {
                assignManager = new AssignShiftManagment(shiftDate);
                UpdateList();
            }
        }

        private void lbShiftPreferences_DoubleClick(object sender, EventArgs e)
        {
            string status = "Selected";
            if (assignManager.DeleteAttendance(lbShiftPreferences, status) == true)
            {
                assignManager = new AssignShiftManagment(shiftDate);
                UpdatePreferencesList();
            }
        }

        private void btnAssignSelectedShift_Click(object sender, EventArgs e)
        {
            if (assignManager.AssignSelectedShift(lbShiftPreferences) == true)
            {
                assignManager = new AssignShiftManagment(shiftDate);
                UpdatePreferencesList();
                UpdateList();
            }
        }
    }
}