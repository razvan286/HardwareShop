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
using Media_Bazaar.LogicLayer.Products;
using Media_Bazaar.LogicLayer.Product;
using System.IO;
using GemBox.Spreadsheet;
using GemBox.Spreadsheet.ConditionalFormatting;

namespace Media_Bazaar
{
    public partial class MainDepot : Form
    {
        private DepotWorkerManagment depotWorkerManagment = new DepotWorkerManagment();
        private DataAccess db = new DataAccess();

        List<int> restockID = new List<int>();
        //List<Product> products = new List<Product>();
        List<RestockRequest> stocks = new List<RestockRequest>();

        public MainDepot()
        {
            InitializeComponent();
        }

        private void MainDepot_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabPages[0].BackColor = Color.FromArgb(116, 208, 252);

            this.cmbDep.Enabled = false;
            this.cmbBrand.Enabled = false;

            Enabled();

            UpdateConfirmedRestockInfo();
            UpdateDepartamentInfo();
            UpdateEmployeeIDInfo();
            UpdateAllConfirmedStockInfo();
            UpdateAllRejectedStockInfo();
            UpdateProductsList();
        }

        private void Enabled()
        {
            this.tbxPProductName.Enabled = false;
            this.tbxProductBrand.Enabled = false;
            this.tbxProductCategory.Enabled = false;
            this.cmbProductDepartment.Enabled = false;
            this.tbxProductID.Enabled = false;
            this.tbxProductQuantity.Enabled = false;
        }

        private void UpdateProductsList() //updating product lists
        {
            this.clbProducts.DataSource = stocks;
            this.clbProducts.DisplayMember = "Info";
        }

        private void UpdateConfirmedRestockInfo() // update confirmed requests info
        {
            foreach (RestockRequestBase rr in depotWorkerManagment.GetIncomingRestockRequests())
            {
                restockID.Add(rr.RequestID);
            }
            
        }


        //----------------------------------Start
        //All buttons connections for the AdminForm 
        //DO NOT Modify THIS !!!
        private void btnIncomingStockTABrequest_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabIncomingStock;
            UpdateConfirmedRestockInfo();
            Enabled();
        }

        private void btnStockTABrequest_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabStock;
        }

        private void btnLogOutTABrequest_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Visible = false;
        }

        private void MainDepot_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnMakeReqTABincomingStock_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabMakeReq;
        }


        private void UpdateAllConfirmedStockInfo() // update all confirmed stock info
        {
            this.clbAllConfirmedRequests.Items.Clear();
            //this.clbData.Items.Clear();

            foreach (RestockRequestBase dBr in depotWorkerManagment.GetIncomingRestockRequests())
            {
                this.clbAllConfirmedRequests.Items.Add(dBr.FullInfo);
                //this.clbData.Items.Add(dBr.FullInfo);
            }
        }

        private void UpdateAllRejectedStockInfo() //update all rejected request info
        {
            this.lbxRejectedRequests.Items.Clear();

            foreach (RestockRequestBase dBr in depotWorkerManagment.GetRejectedRestockRequests())
            {
                this.lbxRejectedRequests.Items.Add(dBr.FullInfo);
            }
        }

        private void btnViewStock_Click(object sender, EventArgs e)// view confirmed requests details
        {
            try
            {
                if (this.clbAllConfirmedRequests.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select a confirmed stock!");
                }
                else
                {
                    tabControl1.SelectedTab = tabInfoStock;
                    UpdateAvailableStockDetails();
                    while (clbAllConfirmedRequests.CheckedIndices.Count > 0)
                    {
                        clbAllConfirmedRequests.SetItemChecked(clbAllConfirmedRequests.CheckedIndices[0], false);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong!!!!!!!");
            }
            
        }

        private void UpdateAvailableStockDetails() // show data for confirmed request restock
        {
            try
            {
                foreach (int i in restockID)
                {
                    if (this.clbAllConfirmedRequests.SelectedItem != null)
                    {
                        string stock = this.clbAllConfirmedRequests.GetItemText(this.clbAllConfirmedRequests.SelectedItem);

                        if (stock.Contains($"ID:{i}"))
                        {
                            this.lblAllStockID.Text = i.ToString();
                            this.lblAllStockName.Text = depotWorkerManagment.GetStockNameById(i);
                            this.lblAllStockType.Text = depotWorkerManagment.GetStockTypeById(i);
                            this.lblAllStockDepartment.Text = depotWorkerManagment.GetDepartmentByStockId(i);
                            this.lblAllStockQuantity.Text = depotWorkerManagment.GetStockQuantityById(i);
                            this.lblAllStockOrderDate.Text = depotWorkerManagment.GetStockOrderDateById(i);
                            this.lblAllStockDeliverDate.Text = depotWorkerManagment.GetStockDeliverDateById(i);
                            this.lblAllStatus.Text = depotWorkerManagment.GetStockStatusById(i);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong!!!!!!!!!!!");
            }
            
        }
        //----------------------------------------Finish


        //------------------------------start
        //Method for changing back color of the selected menu
        public void ChangeMenuColor(TabControl tc)
        {
            if (tc.SelectedTab == tabMakeReq)
            {
                btnMakeReqTABrequest.BackColor = Color.FromArgb(32, 126, 177);
            }
            else
            {
                if (tc.SelectedTab == tabIncomingStock || tc.SelectedTab == tabIncomingStockDetails)
                {
                    btnIncomingStockTABincomingStock.BackColor = Color.FromArgb(32, 126, 177);
                    btnIncomingStockTABincomingStockDet.BackColor = Color.FromArgb(32, 126, 177);
                }
                else
                {
                    if (tc.SelectedTab == tabStock || tc.SelectedTab == tabInfoStock)
                    {
                        btnStockTABstock.BackColor = Color.FromArgb(32, 126, 177);
                        btnStockTABstockInfo.BackColor = Color.FromArgb(32, 126, 177);
                    }
                }
            }
        }
        private void timerChangeMenuColor_Tick(object sender, EventArgs e)
        {
            ChangeMenuColor(tabControl1);
        }

        private void btnIncomingStockTABincomingStock_Click(object sender, EventArgs e)
        {
            UpdateConfirmedRestockInfo();
        }

        private void BtnMakeRequest_Click(object sender, EventArgs e) //senf stock request
        {
            string category = "";
            string brand = "";
            string idEmp = "";
            string orderDate;
            string orderDeliver = "";
            string name = "";
            string quantity = "";
            string department = "";

            idEmp = this.tbxEmployeeID.Text;
            name = this.tbxProductName.Text;
            category = this.cmbProductCategory.Text.ToString();
            brand = this.cmbProductBrand.Text.ToString();
            department = this.cmbDepartment.Text.ToString();
            quantity = this.tbxStockQuantity.Text;
            orderDeliver = this.dtpDateDeliver.Value.ToString("dd/MM/yyyy");
            orderDate = DateTime.Today.ToString("dd/MM/yyyy");

            try
            {
                if (tbxEmployeeID.Text != " " && tbxProductName.Text != " " && tbxStockQuantity.Text != "" &&
                dtpDateDeliver.Value != null && cmbProductBrand.Text != " " && cmbProductCategory.Text != " "
                && cmbDepartment.Text != " ")
                {
                    depotWorkerManagment.MakeRequest(idEmp, name, category, brand, department, quantity, orderDate, orderDeliver);
                    clearBoxes1();
                }
                else
                {
                    MessageBox.Show("All fields must be filled!");
                    clearBoxes1();
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong!!!!!!!!");
            }
        }

        private void clearBoxes1()
        {
            this.cmbProductCategory.Text = " ";
            this.tbxProductName.Text = " ";
            this.tbxStockQuantity.Text = " ";
            this.cmbDepartment.Text = " ";
            this.cmbProductBrand.Text = " ";
        }

        private void UpdateDepartamentInfo() // update combo boxes for department
        {
            try
            {
                foreach (DepartmentModel dBD in depotWorkerManagment.GetDepartments())
                {
                    this.cmbDepartment.Items.Add(dBD.DepartamentName);
                    this.cmbDep.Items.Add(dBD.DepartamentName);
                    this.cmbProductDepartment.Items.Add(dBD.DepartamentName);
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong!!!!!");
            }
            
        }

        private void UpdateEmployeeIDInfo() //update depot ID
        {
            this.tbxEmployeeID.Text = depotWorkerManagment.GetDepotID();
        }

        private void BtnSearchForProduct_Click(object sender, EventArgs e) //search for products
        {
            try
            {
                if (this.cmbShowProducts.Text == "Brand") //searching be brand
                {
                    stocks = db.GetProductInfoByBrand(this.cmbBrand.Text);
                    if (stocks.Count == 0)
                    {
                        MessageBox.Show("We do not have product for thi brand.");
                        this.cmbBrand.Text = "";
                    }
                    else
                    {
                        UpdateProductsList();
                        UpdateDetails();
                        this.clbProducts.Visible = true;
                        this.btnViewProductsDetails.Visible = true;
                    }
                }

                if (this.cmbShowProducts.Text == "Department") //searching by department
                {
                    stocks = db.GetProductInfoByDepartment(this.cmbDep.Text);
                    if (stocks.Count == 0)
                    {
                        MessageBox.Show("We do not have product in this department.");
                        this.cmbDep.Text = "";
                    }
                    else
                    {
                        UpdateProductsList();
                        UpdateDetails();
                        this.clbProducts.Visible = true;
                        this.btnViewProductsDetails.Visible = true;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

        }

        private void UpdateDetails() // update product data
        {
            try
            {
                foreach (int i in restockID)
                {
                    if (this.clbProducts.SelectedItem != null)
                    {
                        string stock = this.clbProducts.GetItemText(this.clbProducts.SelectedItem);

                        if (stock.Contains($"ID:{i}"))
                        {
                            this.tbxProductID.Text = i.ToString();
                            this.tbxPProductName.Text = depotWorkerManagment.GetStockNameById(i);
                            this.tbxProductCategory.Text = depotWorkerManagment.GetStockTypeById(i);
                            this.cmbProductDepartment.Text = depotWorkerManagment.GetDepartmentByStockId(i);
                            this.tbxProductQuantity.Text = depotWorkerManagment.GetStockQuantityById(i);
                            this.tbxProductBrand.Text = depotWorkerManagment.GetBrandByStockId(i);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong!!!!");
            }
            
        }

        private void BtnViewProductsDetails_Click(object sender, EventArgs e) // view products details
        {
            try
            {
                if (this.clbProducts.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select a product!");
                }
                else
                {
                    tabControl1.SelectedTab = this.tabIncomingStockDetails;
                    UpdateDetails();
                    while (clbProducts.CheckedIndices.Count > 0)
                    {
                        clbProducts.SetItemChecked(clbProducts.CheckedIndices[0], false);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong!!!!!!!!!!");
            }
            
        }

        private void BtnEditData_Click(object sender, EventArgs e)
        {
            this.tbxPProductName.Enabled = true;
            this.tbxProductBrand.Enabled = true;
            this.tbxProductCategory.Enabled = true;
            this.cmbProductDepartment.Enabled = true;
            this.tbxProductID.Enabled = false;
            this.tbxProductQuantity.Enabled = true;
        }

        private void BtnSaveData_Click(object sender, EventArgs e) // save changes
        {
            try
            {
                depotWorkerManagment.UpdateData(Convert.ToInt32(this.tbxProductID.Text), this.tbxPProductName.Text, this.tbxProductBrand.Text,
                this.cmbProductDepartment.Text, this.tbxProductCategory.Text, Convert.ToInt32(this.tbxProductQuantity.Text));

                UpdateProductsList();

                MessageBox.Show("Product Data is Updated.");

                Enabled();
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.ToString());
            }
        }

        private void BtnDeleteData_Click(object sender, EventArgs e) //delete product
        {
            try
            {
                depotWorkerManagment.DeleteProduct(Convert.ToInt32(this.tbxProductID.Text));

                UpdateProductsList();

                MessageBox.Show("Product is Deleted.");
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
        }

        private void CmbShowProducts_SelectedIndexChanged(object sender, EventArgs e) //show product by brand/department
        {
            if (this.cmbShowProducts.Text == "Department")
            {
                this.cmbDep.Enabled = true;
                this.cmbBrand.Enabled = false;
            }
            else if (this.cmbShowProducts.Text == "Brand")
            {
                this.cmbDep.Enabled = false;
                this.cmbBrand.Enabled = true;
            }
            else
            {
                MessageBox.Show("Select type to show products!!");
            }
        }

        private void ExportDataToExcel(List<RestockRequest> rr)// export products data to excel file
        {
            DataTable dt = new DataTable();

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            var workbook = new ExcelFile();
            var worksheet = workbook.Worksheets.Add("Exported from messages");

            dt.Columns.Add("RequestID", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Brand", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Departament", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Date Of Order", typeof(string));
            dt.Columns.Add("Date Of Deliver", typeof(string));


            foreach (var table in rr)
            {
                dt.Rows.Add(table.RequestID, table.ProductName, table.Brand, table.Category, table.Departament,
                    table.Quantity, table.DateOfOrder, table.DateOfDelivery);
            }

            // Insert DataTable to an Excel worksheet.
            worksheet.InsertDataTable(dt,
                new InsertDataTableOptions()
                {
                    ColumnHeaders = true,
                    StartRow = 0
                });

            workbook.Save("ProductData.xlsx");

        }

        private void BtnExportToExcel_Click(object sender, EventArgs e) //exporting to excel button
        {
            try
            {
                List<RestockRequest> r = db.GetProductData();

                ExportDataToExcel(r);

                MessageBox.Show("Product Data Downloaded");
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
        }

        private void ExportDataToCSV(List<RestockRequest> rr, string path) //exporting product data to csv
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("RequestID", typeof(int));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("Brand", typeof(string));
                dt.Columns.Add("Category", typeof(string));
                dt.Columns.Add("Departament", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Date Of Order", typeof(string));
                dt.Columns.Add("Date Of Deliver", typeof(string));


                foreach (var table in rr)
                {
                    dt.Rows.Add(table.RequestID, table.ProductName, table.Brand, table.Category, table.Departament,
                        table.Quantity, table.DateOfOrder, table.DateOfDelivery);
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

        private void BtnExportDataToCSV_Click(object sender, EventArgs e) //exporting to csv button
        {
            try
            {
                List<RestockRequest> r = db.GetProductData();
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                ExportDataToCSV(r, path + "\\ProductData.CSV");

                MessageBox.Show("Product Data Downloaded");
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
        }



    }
}
