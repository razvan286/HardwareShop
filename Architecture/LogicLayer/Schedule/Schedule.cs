using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Media_Bazaar
{
    public class Schedule : ScheduleBase
    {
        //fields
        public int EmployeeId { get; private set; }
        public String Date { get; private set; }
        public String Shift { get; private set; }
        public String Attendance { get; private set; }
        public int ShiftId { get; private set; }
        public List<ScheduleBase> allSchedules { get; private set; }
        public string Status { get; private set; }

        //methods

        // return all schedules
        public override void GetAllSchedules()
        {
            base.GetAllSchedules();
        }

        /*public void GetAttendance(LinkLabel lbl, int emplId)
        {
            if(this.Attendance == "PRESENT" && this.EmployeeId == emplId)
            {
                lbl.BackColor = Color.LightGreen;
            }
            else
            {
                if(this.Attendance == "LATE" && this.EmployeeId == emplId)
                {
                    lbl.BackColor = Color.Yellow;
                }
                else
                {
                    if(this.Attendance == "ABSENT" && this.EmployeeId == emplId)
                    {
                        lbl.BackColor = Color.Red;
                    }
                }

            }
        }*/
    }
}
