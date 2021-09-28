using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WestfallMVCWebProject.Models;
using WestfallClassLibrary;


namespace WestfallMVCWebProject.Data
{
    public class ApplicationDbContext : IdentityDbContext  //bring models/classes into the database
    {
        public DbSet<FacilityAdmin> FacilityAdmin { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Facility> Facility { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Vaccine> Vaccine { get; set; }
        public DbSet<VaccineShipment> VaccineShipment { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)//START CONSTRUCTOR
           : base(options)
        {

        }

    }//END CLASS

}//END NAMESPACE
