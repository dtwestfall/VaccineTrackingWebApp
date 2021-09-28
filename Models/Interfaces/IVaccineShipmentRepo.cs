using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestfallClassLibrary;

namespace WestfallMVCWebProject.Models
{
    //INTERFACE HOLDS HEADERS FOR ACTIONS YOUR GOING TO TAKE
    public interface IVaccineShipmentRepo  //interface represents the conduit connecting classes and controllers allowing data access to contollers.
    {
        //Method signatures (What needs to be done, not how it should be done)
        
        List<VaccineShipment> ListAllVaccineShipments();
        int AddVaccineShipment(VaccineShipment vaccineShipment);
    }
}