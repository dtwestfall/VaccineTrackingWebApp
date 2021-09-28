using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestfallClassLibrary;
using WestfallMVCWebProject.Models;
using WestfallMVCWebProject.ViewModels;

namespace WestfallMVCWebProject.Controllers
{
    public class VaccineShipmentController : Controller
    {
        private readonly IVaccineShipmentRepo iVaccineShipmentRepo;
        private readonly IFacilityRepo iFacilityRepo;

        //Need a constructor for every controller
        public VaccineShipmentController(IVaccineShipmentRepo vaccineShipmentRepo, IFacilityRepo facilityRepo)
        {
            this.iVaccineShipmentRepo = vaccineShipmentRepo;
            this.iFacilityRepo = facilityRepo;
        } 
       
        //Start Methods -------------------------------------------------------------------------------------------------------------
        public IActionResult ListAllVaccineShipments()
        {
            List<VaccineShipment> allVaccineShipments = iVaccineShipmentRepo.ListAllVaccineShipments();

            return View(allVaccineShipments);
        }

        [HttpGet]
        public IActionResult AddVaccineShipment()
        {
            ViewData["AllFacilities"] = new SelectList(iFacilityRepo.ListOfAllFacilities(), "FacilityID", "FacilityName");

            return View();
        }

        [HttpPost]
        public IActionResult AddVaccineShipment(AddVaccineShipmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                VaccineShipment vaccineShipment =
                    new VaccineShipment(
                        viewModel.FacilityID,
                        viewModel.VaccineID,
                        viewModel.StartDate.Value,
                        viewModel.NumberOfVaccinesShipped.Value,
                        viewModel.EndDate.Value
                    );

                int vaccineShipmentID = iVaccineShipmentRepo.AddVaccineShipment(vaccineShipment);

                vaccineShipment.VaccineShipmentID = vaccineShipmentID;

                return RedirectToAction("ListAllVaccineShipments");
            }
            else
            {
                ViewData["AllFacilities"] = new SelectList(iFacilityRepo.ListOfAllFacilities(), "FacilityID", "FacilityName");

                return View(viewModel);
            }
        }

        //Adding a View to a list all
        [Authorize(Roles = "FacilityAdmin")]

        //Method that gets user input
        [HttpGet]
        public ViewResult SearchVaccineShipments()
        {
            //Dynamic Dropdowns from database
            //List of all facilities 
            ViewData["AllFacilities"] = new SelectList(iFacilityRepo.ListOfAllFacilities(), "FacilityID", "FacilityName"); //List of items, value (ID), text (Facility name)

            SearchVaccineShipmentsViewModel viewModel = new SearchVaccineShipmentsViewModel();

            return View(viewModel);
        }

        [HttpPost]
        //Method that gives reuslt 
        public ViewResult SearchVaccineShipments(SearchVaccineShipmentsViewModel viewModel )
        {
            List<VaccineShipment> allVaccineShipments = iVaccineShipmentRepo.ListAllVaccineShipments();

            if (viewModel.EndDate != null)
            {
                allVaccineShipments = allVaccineShipments.Where(a => a.StartDate >= viewModel.StartDate).ToList();
            }

            if (viewModel.StartDate != null)
            {
                allVaccineShipments = allVaccineShipments.Where(a => a.EndDate <= viewModel.EndDate).ToList();
            }

            if (viewModel.VaccineID != null)
            {
                allVaccineShipments = allVaccineShipments.Where(a => a.VaccineShipmentID == viewModel.VaccineID).ToList();
            }

            if (viewModel.FacilityID != null)
            {
                allVaccineShipments = allVaccineShipments.Where(a => a.FacilityID == viewModel.FacilityID).ToList();
            }

            viewModel.ResultListOfVaccineShipments = allVaccineShipments;

            ViewData["AllFacilities"] = new SelectList(iFacilityRepo.ListOfAllFacilities(), "FacilityID", "FacilityName"); //List of items, value (ID), text (Facility name)

            return View(viewModel);
        }

    }//End Class
}//End Namespace 
