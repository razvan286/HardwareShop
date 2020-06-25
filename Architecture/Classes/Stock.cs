using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar.Classes
{
    public abstract class Stock
    {
        //Instance variables
        protected String stockType;
        protected int quantity;

        //Constructors
        public Stock()
        {

        }
        public Stock(string type, int quantity)
        {
            this.stockType = type;
            this.quantity = quantity;
        }

        //Properties
        public virtual string Gettype
        {
            get { return this.stockType; }
        }
        public virtual int GetQuantity
        {
            get { return this.quantity; }
        }

        public List<DBStock> AllNeededStocks
        { get; private set; }

        //Methods
        public virtual String GetInfo()
        {
            return $"{this.stockType} , needed quantity is : {this.quantity}.";
        }

        public void GetAllRequests()
        {
            try
            {
                DataAccess da = new DataAccess();
                AllNeededStocks = da.GetAllNeededStocks();
            }
            catch
            {
                AllNeededStocks = new List<DBStock>();
            }

        }
    }
}
