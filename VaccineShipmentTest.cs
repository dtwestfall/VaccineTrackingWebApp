using WestfallMVCWebProject.Models;
using WestfallMVCWebProject.Controllers;
using System;
using Xunit;
using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WestfallMVCWebProject.ViewModels;
using WestfallClassLibrary;

namespace WestfallTestProject1
{
    public class VaccineShipmentTest     //START PROGRAM-------
    {
        //Global Variables
        private readonly Mock<IVaccineShipmentRepo> mockVaccineShipmentRepo;
        private readonly Mock<IFacilityRepo> mockFacilityRepo;
        private readonly VaccineShipmentController controller;

        //-------------------------------------------------------------------


        public VaccineShipmentTest()//START INSTANTIATION METHOD
        {
            mockVaccineShipmentRepo = new Mock<IVaccineShipmentRepo>();
            mockFacilityRepo = new Mock<IFacilityRepo>();
            controller = new VaccineShipmentController(mockVaccineShipmentRepo.Object, mockFacilityRepo.Object);
        }
        //END INSTANTIATION METHOD-------------------------------------------

        [Fact]
        //Arrange
        public void ShouldAddVaccineShipment() // Happy Path: Everything goes well
        {
            AddVaccineShipmentViewModel viewModel =
                new AddVaccineShipmentViewModel 
                { 
                    FacilityID = 1,
                    VaccineID = 101,
                    StartDate = new DateTime(2021, 9, 19),
                    EndDate = new DateTime(2021, 9, 25),
                    NumberOfVaccinesShipped = 1000
                };

            DateTime expectedEndDate = new DateTime(2021, 9, 26);

            VaccineShipment vaccineShipment =
               new VaccineShipment();
            int mockVaccineShipmentID = 9999;

            mockVaccineShipmentRepo.Setup(m => m.AddVaccineShipment(It.IsAny<VaccineShipment>()))
                .Returns(mockVaccineShipmentID);


            mockVaccineShipmentRepo.Setup(
                m => m.AddVaccineShipment(It.IsAny<VaccineShipment>()))
                .Returns(mockVaccineShipmentID)
                .Callback<VaccineShipment>(vs => vaccineShipment = vs);

            //Act
            controller.AddVaccineShipment(viewModel);

            //Assert
            mockVaccineShipmentRepo.Verify(
                m => m.AddVaccineShipment(It.IsAny<VaccineShipment>()), Times.Exactly(1)
                );

            Assert.Equal(mockVaccineShipmentID, vaccineShipment.VaccineShipmentID);
            //Assert.Equal(expectedEndDate, vaccineShipment.EndDate);
        }

        //Sad Path: ShouldNotAddVaccineShipment()
        

        [Fact]  
        public void ShouldListAllVaccineShipments() 
        {
            //AAA Testing: We expect something to happen; compare expected against actual

            //1. Arrange
            int expectedNumberOfShipments = 6;
            int actualNumberOfShipments = 0;
            List<VaccineShipment> mockVaccineShipments = CreateMockVaccineShipmentData(); //Pass in our Mock Data 
            
            //Inject mock data into the controller method 
            mockVaccineShipmentRepo.Setup(m => m.ListAllVaccineShipments()).Returns(mockVaccineShipments); //method inside of a method via a lamda expression DO NOT UNDERSTAND!
       
            //2. Act (actual) CREATE NEW RESULT VARIABLE, CALL CONTROLLER
            ViewResult result = controller.ListAllVaccineShipments() as ViewResult; //get back result 
           
            //Look inside the returned resuly object (List) FROM CONTROLLER
            List<VaccineShipment> actualVaccineShipments = result.Model as List<VaccineShipment>;
           
            //Count the List (actualNumberOfShipments)
            actualNumberOfShipments = actualVaccineShipments.Count;
           
            //3. Assert (Either fails or succeeds)
            Assert.Equal(expectedNumberOfShipments, actualNumberOfShipments);

        }

        [Fact]
        public void ShouldSearchVaccineShipmentsByStartDate()
        {
            //Arrange(& Declare)
            int expectedNumberOfShipments = 0;
            int actualNumberOfShipments = 0;
            List<VaccineShipment> mockVaccineShipments = CreateMockVaccineShipmentData(); //Pass in mock data

            //Adding in new variables for datetime
            DateTime? startDate = new DateTime(2021, 4, 11);
            DateTime? endDate = null;
            mockVaccineShipmentRepo.Setup(m => m.ListAllVaccineShipments()).Returns(mockVaccineShipments);

            mockFacilityRepo.Setup(m => m.ListOfAllFacilities())
                .Returns(new List<Facility>());

            SearchVaccineShipmentsViewModel viewModel = new SearchVaccineShipmentsViewModel();
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FacilityID = null;
            viewModel.VaccineID = null;

            //Act
            ViewResult result = controller.SearchVaccineShipments(viewModel) as ViewResult; //get back result 
            SearchVaccineShipmentsViewModel resultmodel = result.Model as SearchVaccineShipmentsViewModel;

            //Lokk inside the Returned Resulty Object (List)
            List<VaccineShipment> actualVaccineShipment = resultmodel.ResultListOfVaccineShipments;//List<VaccineShipment>; // Count the List(actualNumberOfShipments)   

            //Count the List (actual number of Shipments)
            expectedNumberOfShipments = actualVaccineShipment.Count;

            //Assert
            Assert.Equal(expectedNumberOfShipments, actualNumberOfShipments);
        }



        [Fact]
        public void ShouldSearchVaccineShipmentsByStartAndEndDates()
        {
            //1. Arrange
            int expectedNumberOfShipments = 2;
            int actualNumberOfShipments = 2;
            List<VaccineShipment> mockVaccineShipments = CreateMockVaccineShipmentData();
            //Inject mock data into the controller method 


            DateTime? startDate = new DateTime(2021, 4, 11);
            DateTime? endDate = new DateTime(2021, 4, 15);
            mockVaccineShipmentRepo.Setup(m => m.ListAllVaccineShipments()).Returns(mockVaccineShipments);

            mockFacilityRepo.Setup(m => m.ListOfAllFacilities())
               .Returns(new List<Facility>());

            SearchVaccineShipmentsViewModel viewModel = new SearchVaccineShipmentsViewModel();
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FacilityID = null;
            viewModel.VaccineID = null;

            //2. Act (actual)
            ViewResult result = controller.SearchVaccineShipments(viewModel); //get back result 
            SearchVaccineShipmentsViewModel resultmodel = result.Model as SearchVaccineShipmentsViewModel;//List<VaccineShipment>; // Count the List(actualNumberOfShipments) 
            List<VaccineShipment> actualVaccineShipment = resultmodel.ResultListOfVaccineShipments;                                                                    //Look inside the returned resulty object (List)
            expectedNumberOfShipments = actualVaccineShipment.Count;

            //3. Assert (Either fails or succeeds)
            Assert.Equal(expectedNumberOfShipments, actualNumberOfShipments);
        }
        
        [Fact]
        public void ShouldSearchVaccineShipmentsByStartAndEndDatesAndVaccine()
        {
            //1. Arrange
            int expectedNumberOfShipments = 0;
            int actualNumberOfShipments = 0;
            List<VaccineShipment> mockVaccineShipments = CreateMockVaccineShipmentData();
            //Inject mock data into the controller method 
            mockVaccineShipmentRepo.Setup(m => m.ListAllVaccineShipments()).Returns(mockVaccineShipments);

            mockFacilityRepo.Setup(m => m.ListOfAllFacilities())
               .Returns(new List<Facility>());

            DateTime? startDate = new DateTime(2021, 4, 11);
            DateTime? endDate = new DateTime(2021, 4, 15);
            int? vaccineID = 12;
            SearchVaccineShipmentsViewModel viewModel = new SearchVaccineShipmentsViewModel();
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FacilityID = null;
            viewModel.VaccineID = vaccineID;

            //2. Act (actual)
            ViewResult result = controller.SearchVaccineShipments(viewModel) as ViewResult; //get back result 
            SearchVaccineShipmentsViewModel resultmodel = result.Model as SearchVaccineShipmentsViewModel;
            List<VaccineShipment> actualVaccineShipment = resultmodel.ResultListOfVaccineShipments; //List<VaccineShipment>; // Count the List(actualNumberOfShipments)                                                                       //Look inside the returned resulty object (List)
            actualNumberOfShipments = actualVaccineShipment.Count;

            //3. Assert (Either fails or succeeds)
            Assert.Equal(expectedNumberOfShipments, actualNumberOfShipments);
        }
        
        [Fact]
        public void ShouldSearchVaccineShipmentsByStartAndEndDatesAndVaccineAndFacility()
        {
            //1. Arrange
            int expectedNumberOfShipments = 0;
            int actualNumberOfShipments = 0;
            List<VaccineShipment> mockVaccineShipments = CreateMockVaccineShipmentData();
            //Inject mock data into the controller method 
            mockVaccineShipmentRepo.Setup(m => m.ListAllVaccineShipments()).Returns(mockVaccineShipments); //check this name

            mockFacilityRepo.Setup(m => m.ListOfAllFacilities())
               .Returns(new List<Facility>());

            DateTime? startDate = new DateTime(2021, 4, 11);
            DateTime? endDate = new DateTime(2021, 4, 15);
            int? vaccineID = 12;
            int? facilityID = 16;
            SearchVaccineShipmentsViewModel viewModel = new SearchVaccineShipmentsViewModel();
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FacilityID = facilityID;
            viewModel.VaccineID = vaccineID;

            //2. Act (actual)
            ViewResult result = controller.SearchVaccineShipments(viewModel) as ViewResult; //get back result 
            SearchVaccineShipmentsViewModel resultmodel = result.Model as SearchVaccineShipmentsViewModel;
            List<VaccineShipment> actualVaccineShipment = resultmodel.ResultListOfVaccineShipments; //List<VaccineShipment>; // Count the List(actualNumberOfShipments)                                                                       //Look inside the returned resulty object (List)
            actualNumberOfShipments = actualVaccineShipment.Count;

            //3. Assert (Either fails or succeeds)
            Assert.Equal(expectedNumberOfShipments, actualNumberOfShipments);
        }
        
        [Fact]
        public void ShouldSearchVaccineShipmentsWithoutFiltering()
        {
            //1. Arrange
            int expectedNumberOfShipments = 0;
            int actualNumberOfShipments = 0;
            List<VaccineShipment> mockVaccineShipments = CreateMockVaccineShipmentData();
            //Inject mock data into the controller method 
            mockVaccineShipmentRepo.Setup(m => m.ListAllVaccineShipments()).Returns(mockVaccineShipments); //check this name

            mockFacilityRepo.Setup(m => m.ListOfAllFacilities())
               .Returns(new List<Facility>());

            DateTime? startDate = new DateTime(2021, 4, 11);
            DateTime? endDate = new DateTime(2021, 4, 15);
            int? vaccineID = 12;
            int? facilityID = 16;
            SearchVaccineShipmentsViewModel viewModel = new SearchVaccineShipmentsViewModel();
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FacilityID = facilityID;
            viewModel.VaccineID = vaccineID;

            //2. Act (actual)
            ViewResult result = controller.SearchVaccineShipments(viewModel) as ViewResult; //get back result 
            SearchVaccineShipmentsViewModel resultmodel = result.Model as SearchVaccineShipmentsViewModel;
            List<VaccineShipment> actualVaccineShipment = resultmodel.ResultListOfVaccineShipments; //List<VaccineShipment>; // Count the List(actualNumberOfShipments)                                                                       //Look inside the returned resulty object (List)
            actualNumberOfShipments = actualVaccineShipment.Count;

            //3. Assert (Either fails or succeeds)
            Assert.Equal(expectedNumberOfShipments, actualNumberOfShipments);
        }


        //MOCKING - Mock data for testing (Moq)--------------------------------------------------------------------------------------------------------------------------------

        public List<VaccineShipment> CreateMockVaccineShipmentData()
        {
               //datatype                 var name                       object                 
            List<VaccineShipment> mockVaccineShipmentsList = new List<VaccineShipment>();  //creates list (constructor) and instantiates said list in object form
            
            //Integrated Development enviornment (VS, Eclipse)
            DateTime startDate = new DateTime(2021, 4, 4); //1. compile / build / syntax error v 2. Logic error

            VaccineShipment vaccineShipment = new VaccineShipment(1, 11, startDate, 15);
            mockVaccineShipmentsList.Add(vaccineShipment);

            vaccineShipment = new VaccineShipment(1, 12, startDate, 25);
            mockVaccineShipmentsList.Add(vaccineShipment);

            vaccineShipment = new VaccineShipment(1, 13, startDate, 35);
            mockVaccineShipmentsList.Add(vaccineShipment);

            startDate = new DateTime(2021, 4, 11);

            vaccineShipment = new VaccineShipment(2, 11, startDate, 101);
            mockVaccineShipmentsList.Add(vaccineShipment);

            DateTime endDate = new DateTime(2021, 4, 15);

            vaccineShipment = new VaccineShipment(2, 12, startDate, 151, endDate);
            mockVaccineShipmentsList.Add(vaccineShipment);

            vaccineShipment = new VaccineShipment(2, 13, startDate, 201, endDate);
            mockVaccineShipmentsList.Add(vaccineShipment);

            return mockVaccineShipmentsList;
        } 
        
//END MOCKING-----------------------------------------------------------------------------------------------------------------------------------------------
   
    }//end class

}//end namespace
