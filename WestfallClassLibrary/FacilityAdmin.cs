using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WestfallClassLibrary
{
    public class FacilityAdmin : ApplicationUser
    {
        
        public int FacilityID { get; set; }
        [ForeignKey("FacilityID")]
        public Facility Facility {get; set; } 

        public FacilityAdmin(string firstname, string lastname, string email, string password, string phone, int facilityID)
            :base(firstname, lastname, email, phone, password)
        {
            this.FacilityID = facilityID;
        }

        public FacilityAdmin()
        {

        }
    }
}
