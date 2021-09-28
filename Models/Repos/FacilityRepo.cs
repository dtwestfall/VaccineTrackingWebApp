using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestfallMVCWebProject.Data;
using WestfallClassLibrary;

namespace WestfallMVCWebProject.Models
{
    public class FacilityRepo : IFacilityRepo
    {
        private readonly ApplicationDbContext database;

        public FacilityRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }
        public List<Facility> ListOfAllFacilities()
        {
            return database.Facility.ToList();
        }
    }
}
