using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WestfallClassLibrary;

namespace WestfallClassLibrary
{
    public class ApplicationUser : IdentityUser
    {
        //built in Id (string)
        //properties

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return (FirstName + " " + LastName); }
        }
        public ApplicationUser(string firstname, string lastname, string email, string phone, string password)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.PhoneNumber = PhoneNumber;
            this.UserName = email;

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            this.PasswordHash = passwordHasher.HashPassword(this, password);

            this.SecurityStamp = Guid.NewGuid().ToString();
            
        }

        //EMPTY CONSTREUCTOR REQUIRED FOR ENTITY FRAMEWORK
        //OBJECT-RELATIONAL MAPPER (ORM): TAKES CLASSES AND MAPS THEM TO THE DATABASE OR CLASSES --> RELATIONS
        public ApplicationUser() { }
    }
}
