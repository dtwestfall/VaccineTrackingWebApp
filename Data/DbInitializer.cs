using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestfallMVCWebProject.Models;
using WestfallClassLibrary;

namespace WestfallMVCWebProject.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider services)
        {   //init
            //Initialization of data (sample data)
            //1.Database

            ApplicationDbContext database = services.GetRequiredService<ApplicationDbContext>();

            //2.Roles
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            //3.Users
            UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();


            string hubManagerRole = "HubManager";
            string facilityAdminRole = "FacilityAdmin";
            string patientRole = "Patient";
            string sysAdminRole = "SysAdmin";

            if (!database.Roles.Any())
            {
                IdentityRole role = new IdentityRole(hubManagerRole);
                roleManager.CreateAsync(role).Wait();

                role = new IdentityRole(facilityAdminRole);
                roleManager.CreateAsync(role).Wait();

                role = new IdentityRole(patientRole);
                roleManager.CreateAsync(role).Wait();

                role = new IdentityRole(sysAdminRole);
                roleManager.CreateAsync(role).Wait();
            }

            if (!database.Facility.Any())
            {
                Facility facility = new Facility("WVU Hospital", "1 Medical Center Dr, Morgantown, Wv, 26506");
                database.Facility.Add(facility);
                database.SaveChanges();

                facility = new Facility("WVU Pharmacy", "1 Pharmacy Center Dr, Morgantown, Wv, 26506");
                database.Facility.Add(facility);
                database.SaveChanges();

                facility = new Facility("WVU ER", "1 ER Center Dr, Morgantown, Wv, 26506");
                database.Facility.Add(facility);
                database.SaveChanges();

               
            }

            if (!database.ApplicationUser.Any())
            {
                DateTime dateOfBirth = new DateTime(2000);
                Patient patient = new Patient("Test", "Patient", "Test.Patient@Test.com", "3040012222",
                    "Test.Patient", "109 Wilson Avenue,Morgantow, WV, 26501", dateOfBirth, 3);
                userManager.CreateAsync(patient).Wait();
                userManager.AddToRoleAsync(patient, patientRole).Wait();

                ApplicationUser applicationUser = new ApplicationUser("Test", "SysAdmin", "Test.Admin@sysAdmin.com", 
                    "3048815235", "Test.SysAdmin");
                userManager.CreateAsync(applicationUser).Wait();
                userManager.AddToRoleAsync(applicationUser, sysAdminRole).Wait();

               FacilityAdmin facilityAdmin = new FacilityAdmin("Test", "HospitalFacilityAdmin", "TestAdminEmail@email.com", "TestPassword1", "3049939339", 1);
                userManager.CreateAsync(facilityAdmin).Wait();
                userManager.AddToRoleAsync(facilityAdmin, facilityAdminRole).Wait();

                facilityAdmin = new FacilityAdmin("Test", "HospitalFacilityAdmin2", "TestAdminEmail@email2.com", "TestPassword2", "3049935423", 2);
                userManager.CreateAsync(facilityAdmin).Wait();
                userManager.AddToRoleAsync(facilityAdmin, facilityAdminRole).Wait();

                facilityAdmin = new FacilityAdmin("Test", "HospitalFacilityAdmin3", "TestAdminEmail@email3.com", "TestPassword3", "3049936479", 3);
                userManager.CreateAsync(facilityAdmin).Wait();
                userManager.AddToRoleAsync(facilityAdmin, facilityAdminRole).Wait();

                FacilityAdmin facilityAdminForFacility =
                   database.FacilityAdmin.Where(fa => fa.Email == "TestAdminEmail@email.com").FirstOrDefault();

                Facility facility = database.Facility.Find(1);
                facility.FacilityAdminID = facilityAdminForFacility.Id;
                database.Facility.Update(facility);
                database.SaveChanges();

                 facilityAdminForFacility =
                   database.FacilityAdmin.Where(fa => fa.Email == "TestAdminEmail@email2.com").FirstOrDefault();

                facility = database.Facility.Find(2);
                facility.FacilityAdminID = facilityAdminForFacility.Id;
                database.Facility.Update(facility);
                database.SaveChanges();

                 facilityAdminForFacility =
                   database.FacilityAdmin.Where(fa => fa.Email == "TestAdminEmail@email3.com").FirstOrDefault();

                facility = database.Facility.Find(3);
                facility.FacilityAdminID = facilityAdminForFacility.Id;
                database.Facility.Update(facility);
                database.SaveChanges();

                //facilityAdminForFacility.FacilityID = 1;
            }

            if (!database.Vaccine.Any())
            {
                Vaccine vaccine = new Vaccine("Pfizer", 3, 2);
                database.Vaccine.Add(vaccine);
                database.SaveChanges();

                vaccine = new Vaccine("Moderna", 4, 2);
                database.Vaccine.Add(vaccine);
                database.SaveChanges();

                vaccine = new Vaccine("Johnson & Johnson", 0, 1);
                database.Vaccine.Add(vaccine);
                database.SaveChanges();
            }
               
            

                if (!database.VaccineShipment.Any())
                {
                    List<VaccineShipment> VaccineShipmentsList = new List<VaccineShipment>();
                    DateTime startDate = new DateTime(2021, 4, 4);

                    VaccineShipment vaccineShipment = new VaccineShipment(1, 1, startDate, 15);
                    database.VaccineShipment.Add(vaccineShipment);

                    database.SaveChanges();

                    vaccineShipment = new VaccineShipment(1, 2, startDate, 25);
                    database.VaccineShipment.Add(vaccineShipment);
                    database.SaveChanges();

                    vaccineShipment = new VaccineShipment(1, 3, startDate, 35);
                    database.VaccineShipment.Add(vaccineShipment);
                    database.SaveChanges();

                    startDate = new DateTime(2021, 4, 11);

                    vaccineShipment = new VaccineShipment(2, 1, startDate, 101);
                    database.VaccineShipment.Add(vaccineShipment);
                    database.SaveChanges();

                    DateTime endDate = new DateTime(2021, 4, 15);

                    vaccineShipment = new VaccineShipment(2, 2, startDate, 151, endDate);
                    database.VaccineShipment.Add(vaccineShipment);
                    database.SaveChanges();

                    vaccineShipment = new VaccineShipment(2, 3, startDate, 201, endDate);
                    database.VaccineShipment.Add(vaccineShipment);
                    database.SaveChanges();

             
                }
            }
        }
    }
