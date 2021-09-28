using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestfallMVCWebProject.Data;
using WestfallClassLibrary;

namespace WestfallMVCWebProject.Models
{
    public class VaccineShipmentRepo : IVaccineShipmentRepo //this class implements the interface AND is required to implemtent the database/update the database etc... 
    {
        private readonly ApplicationDbContext database;

        public VaccineShipmentRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public int AddVaccineShipment(VaccineShipment vaccineShipment)
        {
            database.VaccineShipment.Add(vaccineShipment);
            database.SaveChanges();
            return vaccineShipment.VaccineShipmentID;
        }

        public List<VaccineShipment> ListAllVaccineShipments()
        {
            return database.VaccineShipment.Include(vs => vs.Vaccine).Include(vs => vs.Facility).ToList();
        }
    }
}
