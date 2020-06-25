namespace Media_Bazaar
{
    public class EmployeeBase
    {
        public virtual AutoGeneratePassword generatePassword { get; set; } = new AutoGeneratePassword();
        protected string username;
        protected string password;
        private string position;

        public string PassWord { get; set; }

        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string Departament { get; set; }

        public int MinHoursPerWeek { get; set; }
        public int MaxHoursPerWeek { get; set; }
        public int CurrentHours { get; set; }
        public double WagePerHour { get; set; }

        public virtual string Username { get { return username = $"{FirstName.ToLower()}.{LastName}@mediabazaar"; }  }
        public virtual string Password { get { return password = generatePassword.autoGeneratePassword(); }  }

        public string PreferedShiftForTheWeek { get; set; }
        public string ReasonsForRelease { get; set; }
        public virtual string Position { get {return this.position; } set { this.position = value; } } 

        public virtual string FullInfo { get { return $"ID:{EmployeeID} {FirstName} {LastName} {DateOfBirth} {Email} {PhoneNumber} {Nationality} {Position} {Departament} {PreferedShiftForTheWeek})"; } }

        
    }
}
