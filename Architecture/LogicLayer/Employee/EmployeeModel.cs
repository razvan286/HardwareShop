using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Media_Bazaar
{
    public class EmployeeModel : EmployeeBase
    {
        public override AutoGeneratePassword generatePassword { get => base.generatePassword; set => base.generatePassword = value; }

        public override string Username => base.Username;
        public override string Password => base.Password;


        public override string FullInfo
        {
            get
            {
                return base.FullInfo;
            }
        }

        public override string Position { get => base.Position; set => base.Position = value; }
    }
}