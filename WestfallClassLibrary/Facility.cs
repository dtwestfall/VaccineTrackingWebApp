using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WestfallClassLibrary
{
    public class Facility
    {
        [Key]
        public int FacilityID { get; set; } //PK

        public string FacilityName { get; set; }

        public string FacilityAddress { get; set; }

        public string FacilityAdminID { get; set; } //FK

        public List<VaccineShipment> VaccineShipmentsToFacility { get; set; }

        [ForeignKey("FacilityAdminID")]
        public FacilityAdmin FacilityAdmin { get; set; } //object-orientation FK Child

        public Facility(string facilityName, string facilityAddress) //constructor 
        {
            this.FacilityName = facilityName;
            this.FacilityAddress = facilityAddress;
            this.VaccineShipmentsToFacility = new List<VaccineShipment>();
        }

        //EMPTY CONSTRUCTOR REQUIRED FOR ENTITY FRAMEWORK 
        //OBJECT-RELATIONAL MAPPER (ORM): TAKES CLASSES AND MAPS THEM TO THE DATABASE OR CLASSES --> RELATIONS
        public Facility() { }
    }
}
