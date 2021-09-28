using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WestfallMVCWebProject.ViewModels
{
    public class AddVaccineShipmentViewModel
    {
        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Number of vaccines shipped is required")]
        public int? NumberOfVaccinesShipped { get; set; }

        public int FacilityID { get; set; }

        public int VaccineID { get; set; }
    }
}
