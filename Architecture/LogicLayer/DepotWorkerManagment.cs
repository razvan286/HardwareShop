using Media_Bazaar.LogicLayer.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Media_Bazaar
{
    public class DepotWorkerManagment
    {
        DataAccess dataAccess = new DataAccess();

        public void DeleteProduct(int id)
        {
            dataAccess.DeleteProduct(id);
        }

        public void UpdateData(int id, string name, string brand, string department, string category, int quantity)
        {
            dataAccess.UpdateProduct(id, name, brand, department, category, quantity);
        }

        public List<RestockRequestBase> GetIncomingRestockRequests()
        {
            return dataAccess.GetAllConfirmedRestock();
        }

        public List<RestockRequestBase> GetRejectedRestockRequests()
        {
            return dataAccess.GetAllRejectedRestock();
        }

        public List<DepartmentModel> GetDepartments()
        {
            return dataAccess.GetAllDepartaments();
        }

        List<Product> products = new List<Product>();
        List<RestockRequest> stocks = new List<RestockRequest>();

        //public bool MakeRestockRequest(string idEmp, string name, string type, string department, string quantity, string orderDate, string orderDeliver)
        //{
        //    if (!String.IsNullOrEmpty(idEmp) && !String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(department) && !String.IsNullOrEmpty(quantity) && !String.IsNullOrEmpty(orderDeliver) && !String.IsNullOrEmpty(type))
        //    {
        //        dataAccess.InsertRequest(Convert.ToInt32(idEmp), name, type, department, Convert.ToInt32(quantity), orderDate, orderDeliver);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public void MakeRequest(string idEmp, string name, string category, string brand, string department, string quantity, string orderDate, string orderDeliver)
        {
            if (this.DoesProductExist(name) != null )
            {
                MessageBox.Show("Product is already exists.");
            }
            else 
            {
                if (!String.IsNullOrEmpty(idEmp) && !String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(department)
                    && !String.IsNullOrEmpty(quantity) && !String.IsNullOrEmpty(orderDeliver) && !String.IsNullOrEmpty(category)
                     && !String.IsNullOrEmpty(brand))
                {
                    dataAccess.InsertRequest(Convert.ToInt32(idEmp), name, category, brand, department, Convert.ToInt32(quantity), orderDate, orderDeliver);
                    MessageBox.Show("Request is sent to the Administration.");
                }
            }
        }

        public string DoesProductExist(string name)
        {
            foreach (RestockRequestBase r in GetIncomingRestockRequests())
            {
                if (r.ProductName == name)
                {
                    return r.ProductName;
                }
            }
            return null;
        }

        public void UpdateData(int id, string newquantity)
        {
            if (!String.IsNullOrEmpty(newquantity))
            {
                dataAccess.UpdataData(id, Convert.ToInt32(newquantity));
            }
        }

        public int GetProductID(string brand)
        {
            stocks = dataAccess.GetProductInfo(brand);
            foreach (RestockRequest s in stocks)
            {
                return s.RequestID;
            }
            return -1;
        }

        public string GetProductBrand(string brand)
        {
            stocks = dataAccess.GetProductInfo(brand);
            foreach (RestockRequest s in stocks)
            {
                return s.Brand;
            }
            return null;
        }

        public string GetProductName(string brand)
        {
            stocks = dataAccess.GetProductInfo(brand);
            foreach (RestockRequest s in stocks)
            {
                return s.ProductName;
            }
            return null;
        }

        public string GetProductCategory(string brand)
        {
            stocks = dataAccess.GetProductInfo(brand);
            foreach (RestockRequest s in stocks)
            {
                return s.Category;
            }
            return null;
        }

        public string GetProductDepartment(string brand)
        {
            stocks = dataAccess.GetProductInfo(brand);
            foreach (RestockRequest s in stocks)
            {
                return s.Departament;
            }
            return null;
        }

        public int GetProductQuantity(string brand)
        {
            stocks = dataAccess.GetProductInfo(brand);
            foreach (RestockRequest s in stocks)
            {
                return s.Quantity;
            }
            return -1;
        }

        public string GetStockNameById(int id)
        {
            return dataAccess.GetDBStockNameById(id);
        }

        public string GetStockTypeById(int id)
        {
            return dataAccess.GetDBStockTypeById(id);
        }

        public string GetDepartmentByStockId(int id)
        {
            return dataAccess.GetDBDepartmentByStockId(id);
        }

        public string GetBrandByStockId(int id)
        {
            return dataAccess.GetDBBrandByStockId(id);
        }

        public string GetStockQuantityById(int id)
        {
            return dataAccess.GetDBStockQuantityById(id);
        }

        public string GetStockOrderDateById(int id)
        {
            return dataAccess.GetDBStockOrderDateById(id);
        }

        public string GetStockDeliverDateById(int id)
        {
            return dataAccess.GetDBStockDeliverDateById(id);
        }

        public string GetStockStatusById(int id)
        {
            return dataAccess.GetDBStockStatusById(id);
        }

        public string GetEmployeeIdByStockId(int id)
        {
            return dataAccess.GetDBEmployeeIdByStockId(id);
        }

        public string GetDepotID()
        {
            return dataAccess.GetDepotID();
        }
    }
}
