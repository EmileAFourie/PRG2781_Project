using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2781_Group_Project
{
    internal class Student
    {
        private String StudentNumber;
        private String Fullname;
        private DateTime DateOfBirth;
        private String Gender;
        private String PhoneNumber;
        private String Address;
        private String ModuleCodes;

        public Student(string studentNumber, string fullname, DateTime dateOfBirth, string gender, string phoneNumber, string address, string moduleCodes)
        {
            StudentNumber1 = studentNumber;
            Fullname1 = fullname;
            DateOfBirth1 = dateOfBirth;
            Gender1 = gender;
            PhoneNumber1 = phoneNumber;
            Address1 = address;
            ModuleCodes1 = moduleCodes;
        }

        public Student()
        {
            
        }

        public string StudentNumber1 { get => StudentNumber; set => StudentNumber = value; }
        public string Fullname1 { get => Fullname; set => Fullname = value; }
        public DateTime DateOfBirth1 { get => DateOfBirth; set => DateOfBirth = value; }
        public string Gender1 { get => Gender; set => Gender = value; }
        public string PhoneNumber1 { get => PhoneNumber; set => PhoneNumber = value; }
        public string Address1 { get => Address; set => Address = value; }
        public string ModuleCodes1 { get => ModuleCodes; set => ModuleCodes = value; }
    }
}
