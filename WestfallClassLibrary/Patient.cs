using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestfallClassLibrary
{
    public class Patient : ApplicationUser
    {
        public string Address { get; set; }
        public DateTime DateOfBirth {get; set; }
        public int? PriorityLevel { get; set; }

        public Patient(string firstname, string lastname, string email, string phone, string password, string address, DateTime dateofBirth, int? prioritylevel = null) : base(firstname, lastname, email, phone, password)
        {
            this.Address = address;
            this.DateOfBirth = dateofBirth;
            if(prioritylevel != null)
            {
                this.PriorityLevel = prioritylevel;
            }
        }

        //EMPTY CONSTREUCTOR REQUIRED FOR ENTITY FRAMEWORK
        //OBJECT-RELATIONAL MAPPER (ORM): TAKES CLASSES AND MAPS THEM TO THE DATABASE OR CLASSES --> RELATIONS
        public Patient() { }
    }
}
