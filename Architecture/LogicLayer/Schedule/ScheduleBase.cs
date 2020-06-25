using System.Collections.Generic;
using System.Windows.Forms;

namespace Media_Bazaar
{
    public class ScheduleBase
    {
        public string Attendance { get; }
        public string Date { get; }
        public int EmployeeId { get; }
        public string Shift { get; }
        public int ShiftId { get; }
        public List<ScheduleBase> allSchedules { get; private set; }
        public string Status { get; }

        public virtual void GetAllSchedules()
        {
            DataAccess db = new DataAccess();
            try
            {
                allSchedules = db.GetAllSchedules();
            }
            catch
            {
                MessageBox.Show("Connection to the server wasn't possible!");
                allSchedules = new List<ScheduleBase>();
            }
        }

        public virtual string FullInfo { get { return $"{Date}-- {Shift}-- EmployeeID: {EmployeeId}--{Status}"; } }
    }
}