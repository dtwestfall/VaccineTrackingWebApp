using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestfallClassLibrary;

namespace WestfallMVCWebProject.Models
{
    public interface IFacilityRepo
    {
        List<Facility> ListOfAllFacilities();
    }
}
