using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WestfallMVCWebProject.Models;
using WestfallClassLibrary;

namespace WestfallMVCWebProject.ViewModels
{
    public class SearchVaccineShipmentsViewModel
    {
        //For user inputs (all are optional)
        public int? VaccineID { get; set; }
        public int? FacilityID { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        //For result
        public List<VaccineShipment> ResultListOfVaccineShipments {get;set;}
    }
}
