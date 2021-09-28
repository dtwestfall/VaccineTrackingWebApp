using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WestfallClassLibrary
{
    public class Vaccine
    {
        [Key]
        public int VaccineID { get; set; }

        public string Name { get; set; }

        public int IntervalBetweenShots { get; set; }

        public int NumberOfShots { get; set; }

        public List<VaccineShipment> VaccineShipments { get; set; }

        public Vaccine(string name, int intervalBetweenShots, int numberOfShots)
        {
            this.Name = name;
            this.IntervalBetweenShots = intervalBetweenShots;
            this.NumberOfShots = numberOfShots;
            this.VaccineShipments = new List<VaccineShipment>();
        }

        //EMPTY CONSTREUCTOR REQUIRED FOR ENTITY FRAMEWORK
        //OBJECT-RELATIONAL MAPPER (ORM): TAKES CLASSES AND MAPS THEM TO THE DATABASE OR CLASSES --> RELATIONS
        public Vaccine() { }
    }
}
