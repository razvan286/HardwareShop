using Media_Bazaar.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;
using GemBox.Spreadsheet;
using GemBox.Spreadsheet.ConditionalFormatting;


namespace Media_Bazaar
{
    public partial class MainAdmin : Form
    {
        AdminManagment adminManagment = new AdminManagment();

        DataAccess db = new DataAccess();

        List<int> employeesID = new List<int>();
        List<int> restockID = new List<int>();
        List<ScheduleBase> cancelledShiftsEmployeeID = new List<ScheduleBase>();



        //Calendar calendar = new Calendar();
        //Schedule schedule;

        public MainAdmin()
        {
            InitializeComponent();
            this.flDays.Click += new EventHandler(this.Flow_Click);
            //schedule = new Schedule();

            //change this if it s going to be laggy
        }

        private void MainAdmin_Load(object sender, EventArgs e)
        {
            //calendar = new Calendar();
            //GUI load
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabPages[0].BackColor = Color.FromArgb(116, 208, 252);

            // I ADDED A NEW METHOD FOR THE CHANGING OF TABS TO REDUCE THE LOADING TIME OF THE FORM

            // --- Schedule Tab ---  

            adminManagment.CreateCalendar(flDays, lblMonthAndYear);
            /*
            schedule.GetAllSchedules();
            calendar.GenerateDayPanel(42, flDays);
            calendar.DisplayCurrentDate(schedule.allSchedules, lblMonthAndYear);
            */
            UpdateEmployeeInfo();
            UpdateDepartamentInfo();
            UpdateRestockInfo();
            UpdateCancelledShiftsListBox();
        }

        //----------------------------------Start
        //All buttons connections for the AdminForm 
        //DO NOT Modify THIS !!!
        private void MainAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnEmployeeManageTABaddProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabAddProfile;
        }

        private void btnRemoveProfTABaddProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabRemoveProfile;
            UpdateEmployeeInfo();
        }

        private void btnAssignToDepTABaddProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabAssignDep;
        }

        private void btnScheduleTABaddProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabSchedule;
        }

        private void btnRestockTABaddProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabRestockReq;
            UpdateRestockInfo();
        }

        private void btnDepartManageTABaddProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabDepartManage;
        }

        private void btnNewProfileTABremoveProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabAddProfile;
        }

        private void btnLogOutTABaddProfile_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Visible = false;
            //this.Dispose();    ??? should be used when the database will be created????
        }
        //----------------------------------------Finish




        //------------------------------start
        //Method for changing back color of the selected menu
        public void ChangeBackColorOfMenus(TabControl tc)
        {
            if (tc.SelectedTab == tabAddProfile || tc.SelectedTab == tabRemoveProfile || tc.SelectedTab == tabAssignDep)
            {
                btnEmployeeManageTABaddProfile.BackColor = Color.FromArgb(32, 126, 177);
                btnEmployeeManageTABassignDep.BackColor = Color.FromArgb(32, 126, 177);
                btnEmployeeManageTABremoveProfile.BackColor = Color.FromArgb(32, 126, 177);
            }
            else
            {
                if (tc.SelectedTab == tabSchedule)
                {
                    btnScheduleTABschedule.BackColor = Color.FromArgb(32, 126, 177);
                }
                else
                {
                    if (tc.SelectedTab == tabRestockReq)
                    {
                        btnRestockReqTABrestock.BackColor = Color.FromArgb(32, 126, 177);
                    }
                    else
                    {
                        if (tc.SelectedTab == tabDepartManage)
                        {
                            btnDepartmentTABdepartManage.BackColor = Color.FromArgb(32, 126, 177);
                        }
                    }
                }
            }
        }
        private void timerChangingMenusColor_Tick(object sender, EventArgs e)
        {
            ChangeBackColorOfMenus(tabControl1);
        }
        //---------------------------------------------------finish



        // ----- SCHEDULE Tab ---
        private void btnPrevMonth_Click_1(object sender, EventArgs e)
        {
            //calendar.PrevMonth(schedule.allSchedules, lblMonthAndYear);
            adminManagment.CalendarPreviousMonth(lblMonthAndYear);
        }

        private void btnToday_Click_1(object sender, EventArgs e)
        {
            //calendar.Today(schedule.allSchedules, lblMonthAndYear);
            adminManagment.CalendarToday(lblMonthAndYear);
        }

        private void btnNextMonth_Click_1(object sender, EventArgs e)
        {
            //calendar.NextMonth(schedule.allSchedules, lblMonthAndYear);
            adminManagment.CalendarNextMonth(lblMonthAndYear);
        }


        DateTime newDate;
        public void Flow_Click(object sender, EventArgs e)
        {
            AssignShift shiftWindow;
            FlowLayoutPanel pnl = (FlowLayoutPanel)sender;
            sender = pnl.Tag;
            Calendar calendar = new Calendar();
            int day = Convert.ToInt32(sender);
            newDate = new DateTime();
            DateTime date = calendar.GetDate();
            if (day != 0)
            {
                //creating the exact day of which panel is clicked
                newDate = new DateTime(date.Year, date.Month, day);

                shiftWindow = new AssignShift(newDate, this);
                shiftWindow.Show();
            }
        }

        private void btnAddNewProfile_Click_1(object sender, EventArgs e)
        {
            string pos = "";

            if (rbAdministrator.Checked)
            {
                pos = "ADMINISTRATOR";
            }
            else if (rbManager.Checked)
            {
                pos = "MANAGER";
            }
            else if (rbDepotWorker.Checked)
            {
                pos = "DEPOT";
            }
            else if (rbEmployee.Checked)
            {
                pos = "EMPLOYEE";
            }

            if (adminManagment.CreateNewProfile(tbFirstName.Text, tbLastName.Text, tbDateOfBirth.Value.ToShortDateString(), tbEmail.Text, tbPhoneNr.Text, tbNationality.Text, pos, tbMinHrs.Text, tbMaxHrs.Text, tbWage.Text) == null)
            {
                MessageBox.Show("Fill in all fields correctly.");
            }
            label13.Text = adminManagment.GetCredentials();

        }

        private void UpdateEmployeeInfo()
        {
            List<EmployeeBase> NotReleasedEmployees = adminManagment.GetNotFiredEmployees();
            foreach (EmployeeBase e in NotReleasedEmployees)
            {
                employeesID.Add(e.EmployeeID);
            }
            checkedListBox2.DataSource = NotReleasedEmployees;
            checkedListBox2.DisplayMember = "FullInfo";

            checkedListBox3.DataSource = NotReleasedEmployees;
            checkedListBox3.DisplayMember = "FullInfo";
        }
        private void UpdateDepartamentInfo()
        {
            cmbDepartments.Items.Clear();
            List<DepartmentModel> departaments = adminManagment.GetAllDepartaments();

            this.clbDepartments.DataSource = departaments;
            this.clbDepartments.DisplayMember = "DepartamentName";

            lbDepartaments.DataSource = departaments;
            lbDepartaments.DisplayMember = "GetInfo";

            foreach (DepartmentModel dBD in departaments)
            {
                this.cmbDepartments.Items.Add(dBD.DepartamentName);
            }
        }
        private void UpdateRestockInfo()
        {
            List<RestockRequestBase> restockRequests = adminManagment.GetAllRestockRequests();
            foreach (RestockRequestBase rr in restockRequests)
            {
                restockID.Add(rr.RequestID);
            }
            checkedListBox1.DataSource = restockRequests;
            checkedListBox1.DisplayMember = "FullInfo";
        }

        private void UpdateCancelledShiftsListBox()
        {
            List<ScheduleBase> cancelledShifts = adminManagment.GetCancelledShifts();
            foreach (ScheduleBase sb in cancelledShifts)
            {
                cancelledShiftsEmployeeID.Add(sb);
            }
            lbxCancelledShifts.Items.Clear();
            lbxCancelledShifts.DataSource = cancelledShiftsEmployeeID;
            lbxCancelledShifts.DisplayMember = "FullInfo";
        }


        private void btnRemoveProfile_Click(object sender, EventArgs e)
        {
            if (checkedListBox2.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select employee");
            }
            else
            {
                if (String.IsNullOrEmpty(tbExtraInformationTABremoveProfile.Text))
                {
                    MessageBox.Show("Please enter information for release");
                }
                else
                {
                    foreach (int i in employeesID)
                    {
                        if (checkedListBox2.SelectedItem != null)
                        {
                            string empl = checkedListBox2.GetItemText(checkedListBox2.SelectedItem);

                            if (adminManagment.FireEmployee(tbExtraInformationTABremoveProfile.Text, i, empl))
                            {
                                MessageBox.Show("Fired");
                                break;
                            }
                            tbExtraInformationTABremoveProfile.Clear();
                        }
                    }
                }
            }
            UpdateEmployeeInfo();
        }

        private void btnRemoveProfTABremoveProfile_Click(object sender, EventArgs e)
        {
            UpdateEmployeeInfo();
        }

        private void Clear()
        {
            this.tbDepName.Text = " ";
            this.tbMaxNr.Text = " ";
            this.tbMinNr.Text = " ";
        }

        private void btnCreateDepartment_Click(object sender, EventArgs e)
        {
            if (adminManagment.CreateDepartment(tbDepName.Text, tbMinNr.Text, tbMaxNr.Text))
            {
                MessageBox.Show("New department is created.");
                UpdateDepartamentInfo();
                Clear();

            }
            else
            {
                MessageBox.Show("Fill in all fields correctly!");
                Clear();
            }
        }

        private void btnAssignToDepartment_Click(object sender, EventArgs e)
        {
            if (checkedListBox3.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select employee");
            }
            else
            {
                if (cmbDepartments.SelectedItem == null)
                {
                    MessageBox.Show("Please select departament");
                }
                else
                {
                    foreach (int i in employeesID)
                    {
                        if (checkedListBox3.SelectedItem != null)
                        {
                            string empl = checkedListBox3.GetItemText(checkedListBox3.SelectedItem);
                            if (adminManagment.AssignEmployeeToDepartment(i, cmbDepartments.SelectedItem.ToString(), empl))
                            {
                                MessageBox.Show("Assigned");
                                break;
                            }
                        }
                    }
                }
            }
            UpdateEmployeeInfo();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select request");

            }
            else
            {
                foreach (int i in restockID)
                {
                    if (checkedListBox1.SelectedItem != null)
                    {
                        string req = checkedListBox1.GetItemText(checkedListBox1.SelectedItem);
                        if (req.Contains($"ID:{i}"))
                        {
                            if (adminManagment.ConfirmRequest(i, req))
                            {
                                
                            }
                            while (checkedListBox1.CheckedIndices.Count > 0)
                            {
                                checkedListBox1.SetItemChecked(checkedListBox1.CheckedIndices[0], false);
                            }
                        }
                    }
                }
                MessageBox.Show("Confirmed");
            }
            UpdateRestockInfo();
        }

        private void btnRestockReqTABrestock_Click(object sender, EventArgs e)
        {
            UpdateRestockInfo();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select request");
            }
            else
            {
                foreach (int i in restockID)
                {
                    if (checkedListBox1.SelectedItem != null)
                    {
                        string req = checkedListBox1.GetItemText(checkedListBox1.SelectedItem);
                        if (req.Contains($"ID:{i}"))
                        {
                            if (adminManagment.RejectRequest(i, req))
                            {
                                
                            }
                            while (checkedListBox1.CheckedIndices.Count > 0)
                            {
                                checkedListBox1.SetItemChecked(checkedListBox1.CheckedIndices[0], false);
                            }
                        }
                    }
                }
                MessageBox.Show("Rejected");
            }
            UpdateRestockInfo();
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabEmail;
        }
        string split = "";
        string subject = "";
        string body = "";
        public void UpdateMailList(string split, string subject)
        {
            lbEmailInbox.Items.Clear();

            foreach (EmailModel mail in adminManagment.GetEmails())
            {
                int id = mail.EmployeeID;

                string employeeName = adminManagment.GetFirstNameOfEmployeeById(id);
                split = mail.Email.Split(new string[] { "Subject:" }, StringSplitOptions.None)[1];
                //body = split.Split(new string[] { "Body:" }, StringSplitOptions.None)[1];
                subject = split.Split(new string[] { "Body:" }, StringSplitOptions.None)[0];
                //rtbEmailBody.Text = body;
                string display = $"{employeeName}-({id}):  {subject} -> Status:{mail.Status}";
                lbEmailInbox.Items.Add(display);
                lbEmailInbox.Items.Add("");
                lbEmailInbox.Items.Add("");
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlHolderContentMail.Visible = false;
            if (tabControl1.SelectedTab == tabEmail)
            {
                UpdateMailList(split, subject);
            }
        }
        EmailModel dbEmail = null;
        private void lbEmailInbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string holder = "";
            pnlHolderContentMail.Visible = true;
            if (lbEmailInbox.SelectedItem != null)
            {
                holder = lbEmailInbox.SelectedItem.ToString();
                foreach (EmailModel mail in adminManagment.GetEmails())
                {

                    int id = mail.EmployeeID;
                    string employeeName = adminManagment.GetFirstNameOfEmployeeById(id);
                    split = mail.Email.Split(new string[] { "Subject:" }, StringSplitOptions.None)[1];
                    string toBeSearched = "Body:";
                    body = split.Substring(split.IndexOf(toBeSearched) + toBeSearched.Length);
                    subject = split.Split(new string[] { "Body:" }, StringSplitOptions.None)[0];

                    if (holder.Contains(mail.EmployeeID.ToString()) && holder.Contains(subject) && mail.Email.Contains(subject))
                    {
                        lblEmailFrom.Text = employeeName;
                        lblEmailDate.Text = mail.Date;
                        lblEmailSubject.Text = subject;
                        rtbEmailBody.Text = body;
                        dbEmail = mail;
                        if (mail.Status == "Read")
                        {
                            btnMarkAsRead.Visible = false;
                        }
                        else
                        {
                            btnMarkAsRead.Visible = true;
                        }
                    }
                }

            }
        }

        private void btnMarkAsRead_Click(object sender, EventArgs e)
        {
            int emailID = -1;
            if (dbEmail != null)
            {
                emailID = dbEmail.EmailID;
                adminManagment.EmailStatusRead(emailID);
            }
            else
            {
                MessageBox.Show("The action could not be made at this time.");
            }
            UpdateMailList(split, subject);
        }

        private void btnDeleteMail_Click(object sender, EventArgs e)
        {
            int emailID = -1;
            if (dbEmail != null)
            {
                emailID = dbEmail.EmailID;
                adminManagment.DeleteEmailById(emailID);
            }
            else
            {
                MessageBox.Show("The action could not be made at this time.");
            }
            UpdateMailList(split, subject);
            pnlHolderContentMail.Visible = false;
        }

        private void btnNewProfTABaddProf_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCreateProf_Click(object sender, EventArgs e)
        {
            tbFirstName.Clear();
            tbLastName.Clear();
            tbEmail.Clear();
            tbPhoneNr.Clear();
            tbMinHrs.Clear();
            tbMaxHrs.Clear();
            tbWage.Clear();
            label13.Text = "";
            tbNationality.Items.Clear();
            MessageBox.Show("The account has been successfully created!");
        }



        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = SheduleGenerating;
        }

        private void btnScheduleGenerator_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = SheduleGenerating;
        }

        public void Clean()
        {
            this.tbDepartment.Text = "";
            this.tbNrAdmins.Text = "";
            this.tbNrManagers.Text = "";
            this.tbNrDepotWorkers.Text = "";
            this.tbNrEmployees.Text = "";
        }

        private void btnGenerateScheduleForTheWeek_Click(object sender, EventArgs e)
        {
            adminManagment.GenerateSchedule(tbDepartment.Text, Convert.ToInt32(tbNrAdmins.Text), Convert.ToInt32(tbNrManagers.Text), Convert.ToInt32(tbNrDepotWorkers.Text), Convert.ToInt32(tbNrEmployees.Text));
            MessageBox.Show("Generated!");

            Clean();
        }

        private void btnReassign_Click(object sender, EventArgs e)
        {
            adminManagment.Reasign();
            MessageBox.Show("Reassigned");
        }

        private void BtnCreateDepartment_Click_1(object sender, EventArgs e) // create new departments
        {
            try
            {
                if (adminManagment.CreateDepartment(tbDepName.Text, tbMinNr.Text, tbMaxNr.Text))
                {
                    MessageBox.Show("New department is created.");
                    UpdateDepartamentInfo();
                    Clear();

                }
                else
                {
                    MessageBox.Show("Fill in all fields correctly!");
                    Clear();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void BtnSearchDep_Click(object sender, EventArgs e) // search for department by name
        {
            try
            {
                List<DepartmentModel> deps = adminManagment.GetAllDepartaments();
                deps = db.GetDepInfo(this.tbxDepName.Text);
                if (deps.Count == 0)
                {
                    MessageBox.Show("We do not have such a department in our company.");
                    this.tbxDepName.Text = "";
                }
                else
                {
                    lbxFoundDepartaments.DataSource = deps;
                    lbxFoundDepartaments.DisplayMember = "GetInfo";
                    this.lbxFoundDepartaments.Visible = true;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void BtnRemoveDep_Click(object sender, EventArgs e) // remove deopartment buttong
        {
            try
            {
                if (this.clbDepartments.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select department first!");
                }
                else
                {
                    string dep = this.clbDepartments.GetItemText(this.clbDepartments.SelectedItem);

                    if (adminManagment.RemoveFDepartment(dep) == true)
                    {
                        MessageBox.Show("Department is removed.");
                    }
                }
                UpdateDepartamentInfo();
                UpdateEmployeeInfo();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void ExportData(List<EmployeeBase> emp)//export employee data
        {
            try
            {
                DataTable dt = new DataTable();

                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("Exported from messages");

                dt.Columns.Add("EmployeeID", typeof(int));
                dt.Columns.Add("FirstName", typeof(string));
                dt.Columns.Add("LastName", typeof(string));
                dt.Columns.Add("DateOfBirth", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Nationality", typeof(string));
                dt.Columns.Add("Departament", typeof(string));

                foreach (var table in emp)
                {
                    dt.Rows.Add(table.EmployeeID, table.FirstName, table.LastName, table.DateOfBirth, table.Email,
                       /* table.PhoneNumber,*/ table.Nationality, table.Departament);
                }

                //// Insert DataTable to an Excel worksheet.
                worksheet.InsertDataTable(dt,
                    new InsertDataTableOptions()
                    {
                        ColumnHeaders = true,
                        StartRow = 0
                    });

                workbook.Save("EmployeeData.xlsx");
            }
            catch
            {
                MessageBox.Show("Something wnt wrong!!!");
            }
            

        }
        private void BtnExportToExcel_Click(object sender, EventArgs e) // export to excel button
        {
            try
            {
                List<EmployeeBase> emp = db.GetNotFiredEmployees(); ;

                ExportData(emp);

                MessageBox.Show("Employee Data Downloaded");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void ExportDataToCSV(List<EmployeeBase> emp, string path) //exporting employee data to csv
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("EmployeeID", typeof(int));
                dt.Columns.Add("FirstName", typeof(string));
                dt.Columns.Add("LastName", typeof(string));
                dt.Columns.Add("DateOfBirth", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Nationality", typeof(string));
                dt.Columns.Add("Departament", typeof(string));

                foreach (var table in emp)
                {
                    dt.Rows.Add(table.EmployeeID, table.FirstName, table.LastName, table.DateOfBirth, table.Email,
                        table.Nationality, table.Departament);
                }

                StreamWriter sw = new StreamWriter(path, false);
                //headers  
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void BtnExportToCSV_Click(object sender, EventArgs e) // export to csv button
        {
            try
            {
                List<EmployeeBase> emp = db.GetNotFiredEmployees(); ;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ExportDataToCSV(emp, path + "\\EmployeeData.CSV");

                MessageBox.Show("Employee Data Downloaded");
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabAddProfile;
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabSchedule;

        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabRestockReq;
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabDepartManage;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Visible = false;
        }
    }
}