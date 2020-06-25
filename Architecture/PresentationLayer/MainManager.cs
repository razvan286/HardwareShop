using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Media_Bazaar.Classes;
using MySql.Data.MySqlClient;

namespace Media_Bazaar
{
    public partial class MainManager : Form
    {
        private ManagerManagment managerManagment = new ManagerManagment();

        public MainManager()
        {
            InitializeComponent();

            Enabled();

            UpdateList(managerManagment.GetAllEmployees());
            UpdateComboBox(managerManagment.GetAllRestockNames());
            CheckFiredAndWorkingChart();
            CheckAttendance();
            CheckRequests();
            CheckPositions();
        }

        private void Enabled()
        {
            this.tbxDateOfBirth.Enabled = false;
            this.tbxEmail.Enabled = false;
            this.tbxNationality.Enabled = false;
            this.tbxPhoneNumber.Enabled = false;
            this.tbxPassword.Enabled = false;
        }

        private void UpdateList(List<EmployeeBase> employees) // update employee list
        {
            checkLbProfile.DataSource = employees;
            checkLbProfile.DisplayMember = "FullInfo";
        }
        private void UpdateComboBox(List<RestockRequestBase> restocks) // update name combo box
        {
            cbRestock.ValueMember = "NameInfo";
            cbRestock.DisplayMember = "NameInfo";
            cbRestock.DataSource = restocks;
        }

        private void MainManager_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabPages[0].BackColor = Color.FromArgb(116, 208, 252);
        }


        //----------------------------------Start
        //All buttons connections for the AdminForm 
        //DO NOT Modify THIS !!!
        private void btnSearchTABemplStats_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabSearchEmpl;
            tbxSearchID.Clear();
            tbxSearchLastname.Clear();
            foreach (var series in chartEmplAttendance.Series)
            {
                series.Points.Clear();
            }
        }

        private void btnLogOutTABdepart_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Visible = false;
        }

        private void MainManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnShiftsTABemplStats_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabShiftStats;
        }

        private void btnDepartStockTABemplStats_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabDepartStats;
        }

        private void btnEmployeesTABshiftStats_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabEmployeeStats;
        }

        private void btnStatisticsTABsearch_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabEmployeeStats;
        }
        //Here information about the employee should be added, in order to display his correct info on the profile TAB.
        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabProfile;
        }
        //----------------------------------------Finish



        //------------------------------start
        //Method for changing back color of the selected menu
        public void ChangeBackColorOfMenus(TabControl tc)
        {
            if (tc.SelectedTab == tabEmployeeStats || tc.SelectedTab == tabShiftStats || tc.SelectedTab == tabDepartStats)
            {
                btnStatsTABdepart.BackColor = Color.FromArgb(32, 126, 177);
                btnStatsTABemployeeStats.BackColor = Color.FromArgb(32, 126, 177);
                btnStatisticsTABshiftStats.BackColor = Color.FromArgb(32, 126, 177);
            }
            else
            {
                if (tc.SelectedTab == tabSearchEmpl || tc.SelectedTab == tabProfile)
                {
                    btnSearchTABprofile.BackColor = Color.FromArgb(32, 126, 177);
                    btnSearchTABsearch.BackColor = Color.FromArgb(32, 126, 177);
                }
            }
        }
        private void timerSelectedMenu_Tick(object sender, EventArgs e)
        {
            ChangeBackColorOfMenus(tabControl1);
        }

        private void btnSearchForSpecificEmployee_Click(object sender, EventArgs e) // search for employee 
        {
            try
            {
                if (cmbSelectSeachMethod.Text == "Last name") //by last name
                {
                    if (managerManagment.GetNotFiredEmployeesByLName(tbxSearchLastname.Text).Count == 0)
                    {
                        MessageBox.Show("Employee with the specified last name cannot be found. He may be fired.");
                        tbxSearchLastname.Clear();
                    }
                    else
                    {
                        UpdateList(managerManagment.GetNotFiredEmployeesByLName(tbxSearchLastname.Text));
                        UpdateInfoByLastname(this.tbxSearchLastname.Text);
                        checkLbProfile.Visible = true;
                        btnViewProfile.Visible = true;
                    }

                }
                else if (cmbSelectSeachMethod.Text == "ID") //by id
                {
                    if (managerManagment.GetNotFiredEmployeesByID(Convert.ToInt32(this.tbxSearchID.Text)).Count == 0)
                    {
                        MessageBox.Show("Employee with the specified ID cannot be found. He may be fired.");
                        tbxSearchID.Clear();
                    }
                    else
                    {
                        UpdateList(managerManagment.GetNotFiredEmployeesByID(Convert.ToInt32(this.tbxSearchID.Text)));
                        UpdateInfoByID(Convert.ToInt32(this.tbxSearchID.Text));
                        checkLbProfile.Visible = true;
                        btnViewProfile.Visible = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong !!!!!!!!!!!!");
            }

        }

        private void UpdateInfoByID(int id) // update employee data by ID
        {
            try
            {
                //display the data in the labels and in the text boxes
                lbUpcomingShifts.Items.Clear();
                DateTime dateNow = DateTime.Today;
                string[] date = dateNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Split('/');

                foreach (EmployeeBase empl in managerManagment.GetNotFiredEmployeesByID(id))
                {
                    this.lblFirstName.Text = empl.FirstName;
                    this.lblLastName.Text = empl.LastName;
                    this.lblPosInCompany.Text = empl.Position;
                    this.lblID.Text = empl.EmployeeID.ToString();
                    this.tbxEmail.Text = empl.Email;
                    this.tbxPhoneNumber.Text = empl.PhoneNumber;
                    this.tbxNationality.Text = empl.Nationality;
                    this.tbxDateOfBirth.Text = empl.DateOfBirth;
                    this.tbxPassword.Text = empl.PassWord;
                    Enabled();
                }

                foreach (ScheduleBase sch in managerManagment.GetSchedulesByEmplId(id))
                {
                    string[] dateShift = sch.Date.Split('/');
                    if (((dateShift[2] == date[2]) && (dateShift[1] == date[1]) && (Convert.ToInt32(dateShift[0]) >= Convert.ToInt32(date[0]))))
                    {
                        //if is not the upcoming day
                        lbUpcomingShifts.Items.Add($"{dateShift[0]}/{dateShift[1]}/{dateShift[2]} -> {sch.Shift}");
                    }
                    else
                    {
                        //if the month has passed
                        if ((Convert.ToInt32(dateShift[1]) != Convert.ToInt32(date[1])) && (Convert.ToInt32(dateShift[1]) >= Convert.ToInt32(date[1])) && (dateShift[2] == date[2]) && ((Convert.ToInt32(dateShift[0]) >= Convert.ToInt32(date[0])) || (Convert.ToInt32(dateShift[0]) < Convert.ToInt32(date[0]))))

                        {
                            lbUpcomingShifts.Items.Add($"{dateShift[0]}/{dateShift[1]}/{dateShift[2]} -> {sch.Shift}");
                        }
                        else
                        {
                            //if the year is different
                            if ((Convert.ToInt32(dateShift[2]) != Convert.ToInt32(date[2])) && (Convert.ToInt32(dateShift[2]) >= Convert.ToInt32(date[2])) && ((Convert.ToInt32(dateShift[1]) >= Convert.ToInt32(date[1])) || (Convert.ToInt32(dateShift[1]) < Convert.ToInt32(date[1]))) && ((Convert.ToInt32(dateShift[0]) >= Convert.ToInt32(date[0])) || (Convert.ToInt32(dateShift[0]) < Convert.ToInt32(date[0]))))
                            {
                                lbUpcomingShifts.Items.Add($"{dateShift[0]}/{dateShift[1]}/{dateShift[2]} -> {sch.Shift}");
                            }
                        }
                    }

                }
                CheckEmployeeAttendance(id);
            }
            catch
            {
                MessageBox.Show("Something went wrong!!!!!!!!!!");
            }

        }

        private void UpdateInfoByLastname(string lastName) // upadte employee data by last Name
        {
            try
            {
                //display the data in the labels
                lbUpcomingShifts.Items.Clear();
                DateTime dateNow = DateTime.Today;

                string[] date = dateNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Split('/');
                int employeeId = -1;

                foreach (EmployeeBase empl in managerManagment.GetEmployeesByLName(lastName))
                {
                    this.lblFirstName.Text = empl.FirstName;
                    this.lblLastName.Text = lastName;
                    this.lblPosInCompany.Text = empl.Position;
                    this.lblID.Text = empl.EmployeeID.ToString();
                    this.tbxEmail.Text = empl.Email;
                    this.tbxPhoneNumber.Text = empl.PhoneNumber;
                    this.tbxNationality.Text = empl.Nationality;
                    this.tbxDateOfBirth.Text = empl.DateOfBirth;
                    this.tbxPassword.Text = empl.PassWord;
                    Enabled();
                    employeeId = empl.EmployeeID;
                }

                foreach (ScheduleBase sch in managerManagment.GetSchedulesByEmplId(employeeId))
                {
                    string[] dateShift = sch.Date.Split('/');
                    if (((dateShift[2] == date[2]) && (dateShift[1] == date[1]) && (Convert.ToInt32(dateShift[0]) >= Convert.ToInt32(date[0]))))
                    {
                        //if is not the upcoming day
                        lbUpcomingShifts.Items.Add($"{dateShift[0]}/{dateShift[1]}/{dateShift[2]} -> {sch.Shift}");
                    }
                    else
                    {
                        //if the month has passed
                        if ((Convert.ToInt32(dateShift[1]) != Convert.ToInt32(date[1])) && (Convert.ToInt32(dateShift[1]) >= Convert.ToInt32(date[1])) && (dateShift[2] == date[2]) && ((Convert.ToInt32(dateShift[0]) >= Convert.ToInt32(date[0])) || (Convert.ToInt32(dateShift[0]) < Convert.ToInt32(date[0]))))

                        {
                            lbUpcomingShifts.Items.Add($"{dateShift[0]}/{dateShift[1]}/{dateShift[2]} -> {sch.Shift}");
                        }
                        else
                        {
                            //if the year is different
                            if ((Convert.ToInt32(dateShift[2]) != Convert.ToInt32(date[2])) && (Convert.ToInt32(dateShift[2]) >= Convert.ToInt32(date[2])) && ((Convert.ToInt32(dateShift[1]) >= Convert.ToInt32(date[1])) || (Convert.ToInt32(dateShift[1]) < Convert.ToInt32(date[1]))) && ((Convert.ToInt32(dateShift[0]) >= Convert.ToInt32(date[0])) || (Convert.ToInt32(dateShift[0]) < Convert.ToInt32(date[0]))))
                            {
                                lbUpcomingShifts.Items.Add($"{dateShift[0]}/{dateShift[1]}/{dateShift[2]} -> {sch.Shift}");
                            }
                        }
                    }
                }
                CheckEmployeeAttendance(employeeId);
            }
            catch
            {
                MessageBox.Show("Something went wrong!!!!!!!!");
            }

        }


        private void CheckFiredAndWorkingChart() // check fired employee
        {
            chartReleasedAndNot.Series["s1"].Points.AddXY("Fired", managerManagment.NumberOfFired());
            chartReleasedAndNot.Series["s1"].Points.AddXY("Working", managerManagment.NumberrOfWorking());
        }

        private void CheckPositions() // check position
        {
            chartPositions.Series["s1"].Points.AddXY("Administrators", managerManagment.NumberOfAdministrators());
            chartPositions.Series["s1"].Points.AddXY("Managers", managerManagment.NumberOfManagers());
            chartPositions.Series["s1"].Points.AddXY("Depot workers", managerManagment.NumberOfDepotWorkers());
        }

        int nrOfPresent = 0;
        int nrOfAbsent = 0;
        int nrOfLate = 0;

        private void CheckAttendance()
        {
            chartAttendance.Series["s1"].Points.AddXY("Present", managerManagment.NumberOfPresent());
            chartAttendance.Series["s1"].Points.AddXY("Absent", managerManagment.NumberOfAbsent());
            chartAttendance.Series["s1"].Points.AddXY("Late", managerManagment.NumberOfLate());
        }

        private void CheckEmployeeAttendance(int id)
        {
            chartEmplAttendance.Series["s1"].Points.AddXY("Present", managerManagment.GetNumberOfPresentByID(id));
            chartEmplAttendance.Series["s1"].Points.AddXY("Absent", managerManagment.GetNumberOfAbsentByID(id));
            chartEmplAttendance.Series["s1"].Points.AddXY("Late", managerManagment.GetNumberOfLateByID(id));

            chartEmplAttendance.Series["s1"]["PieLabelStyle"] = "Disabled";
        }

        private void CheckRequests()
        {
            chartRequests.Series["s1"].Points.AddXY("Confirmed", managerManagment.NumberOfConfirmedRequests());
            chartRequests.Series["s1"].Points.AddXY("Rejected", managerManagment.NumberOfRejectedRequests());
            chartRequests.Series["s1"].Points.AddXY("Waiting", managerManagment.NumberOfWaitingRequests());
        }

        private void btnSearchTABsearch_Click(object sender, EventArgs e)
        {
            checkLbProfile.Visible = false;
            btnViewProfile.Visible = false;
            tbxSearchLastname.Enabled = true;
            tbxSearchID.Enabled = true;

        }

        private void cmbSelectSeachMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectSeachMethod.Text == "Last name")
            {
                tbxSearchID.Enabled = false;
                tbxSearchLastname.Enabled = true;
            }
            else
            {
                tbxSearchLastname.Enabled = false;
                tbxSearchID.Enabled = true;
            }
        }

        private void btnEmployeesTABemplStats_Click(object sender, EventArgs e)
        {

        }

        private void BtnEditData_Click(object sender, EventArgs e)
        {
            this.tbxDateOfBirth.Enabled = true;
            this.tbxEmail.Enabled = true;
            this.tbxNationality.Enabled = true;
            this.tbxPhoneNumber.Enabled = true;
            this.tbxPassword.Enabled = true;
        }

        private void BtnSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                managerManagment.UpdateData(Convert.ToInt32(this.lblID.Text), this.tbxDateOfBirth.Text, this.tbxNationality.Text,
                this.tbxEmail.Text, this.tbxPhoneNumber.Text, this.tbxPassword.Text);

                MessageBox.Show("Employee Data is Updated.");

                this.tbxDateOfBirth.Enabled = false;
                this.tbxEmail.Enabled = false;
                this.tbxPhoneNumber.Enabled = false;
                this.tbxPassword.Enabled = false;
                this.tbxNationality.Enabled = false;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void btnRestock_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRestock_Click_1(object sender, EventArgs e)
        {
            string name = cbRestock.Text;
            chartRestock.Series["restock"].Points.AddXY(Convert.ToString(name), managerManagment.GetRestockQuantity(name));
        }
    }
}