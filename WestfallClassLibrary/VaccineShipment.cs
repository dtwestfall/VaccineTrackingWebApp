using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WestfallClassLibrary;

namespace WestfallClassLibrary
{
    public class VaccineShipment
    {
        [Key]
        public int VaccineShipmentID { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public int FacilityID { get; set; }

        public int VaccineID { get; set; }

        public int NumberOfVaccinesUsed { get; set; }

        public int NumberOfVaccinesShipped { get; set; }

        

        //Object-Oriented Connection below
        [ForeignKey("FacilityID")]
        public Facility Facility { get; set; }
        [ForeignKey("VaccineID")]
        public Vaccine Vaccine { get; set; }

        //Below is a constructor method (instantiating the object) 
        public VaccineShipment(int facilityID, int vaccineID, DateTime startDate, int numberofVaccinesShipped, DateTime? endDate = null)
        {
            this.FacilityID = facilityID;
            this.VaccineID = vaccineID;
            this.StartDate = startDate;
            if (endDate != null)
            {
                this.EndDate = endDate;
            }
            else
            {
                this.EndDate = startDate.AddDays(7);
            }
            this.NumberOfVaccinesShipped = numberofVaccinesShipped;
            this.NumberOfVaccinesUsed = 0;
        }

        //EMPTY CONSTREUCTOR REQUIRED FOR ENTITY FRAMEWORK
        //OBJECT-RELATIONAL MAPPER (ORM): TAKES CLASSES AND MAPS THEM TO THE DATABASE OR CLASSES --> RELATIONS
        public VaccineShipment() { }
    }  
}
