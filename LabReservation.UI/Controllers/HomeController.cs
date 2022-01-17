using AutoMapper;
using LabReservation.Data.Entities;
using LabReservation.Services.Interfaces;
using LabReservation.UI.Areas.Admin.Models;
using LabReservation.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

 


namespace LabReservation.UI.Controllers
{
    public class HomeController : Controller
    {


        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        private readonly ILabService _labService;
        private readonly ITestService _testService;
        private readonly IBranchService _branchService;
        private readonly IReservationService _reservationService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IAreaService areaService,
            IMapper mapper,
            ICityService cityService,
            ILabService labService,
            ITestService testService,
            IBranchService branchService,
            IReservationService reservationService,
            ILogger<HomeController> logger
            )
        {
            _areaService = areaService;
            _mapper = mapper;
            _cityService = cityService;
            _labService = labService;
            _testService = testService;
            _branchService = branchService;
            _reservationService = reservationService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Admin()
        {
            return View();
        }







        public async Task<IActionResult> Reserve()
        {
            var model = new Models.ReservationModel();

            var cities = await _cityService.getAllCities();
            var citiesModel = _mapper.Map<List<City>, List<CityModel>>(cities);
            model.Cities = citiesModel;

            var areas = await _areaService.getAllAreas();
            var areasModel = _mapper.Map<List<Area>, List<AreaModel>>(areas);
            model.Areas = areasModel;

            var labs = await _labService.getAllLabs();
            var labsModel = _mapper.Map<List<Lab>, List<LabModel>>(labs);
            model.Labs = labsModel;

            var branchs = await _branchService.getAllBranchs();
            var branchsModel = _mapper.Map<List<Branch>, List<BranchModel>>(branchs);
            model.Branches = branchsModel;

            var allTestLabs = await _labService.getAllLabTests();


            model.SelectedLabTest = allTestLabs.Select(o => new LabTestSelectModel
            {
                Fees = o.Fees,
                LabId = o.LabId,
                TestId = o.TestId,
                TestName = o.Test.Name,
                LabName = o.Lab.Name,
                Selected = false,
                LabTestId = o.LabTestId
            }).ToList();



            return View(model);
        }

        // POST: AreasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(Models.ReservationModel model)
        {
            model.ReserveTime = DateTime.Now;
            var reservation = _mapper.Map<Models.ReservationModel, Reservation>(model);

           

            if (reservation.LocationType)
            {
                var lab = await _labService.getLabById(reservation.LabId);
                reservation.TotalAmount = lab.HomeFees;
            }



            var labtestSelected = model.SelectedLabTest.Where(e => e.Selected);

            foreach (var item in labtestSelected)
            {
                reservation.TotalAmount += item.Fees;
            }

           
            var reserId = _reservationService.addReservation(reservation);


            foreach (var item in labtestSelected)
            {
                item.ReservationId = reserId;
            }
            var reservationDetails = _mapper.Map<List<LabTestSelectModel>, List<ReservationDetail>>(labtestSelected.ToList());

            await _reservationService.addReservationDetails(reservationDetails);

            model.ReservationId = reserId;
            return View(model);

        }



        public async Task<ActionResult> GetLabHomeFees(int? labId)
        {

            var tests = await _labService.getLabById(labId.Value);


            return new JsonResult(tests.HomeFees);
        }

        public async Task<ActionResult> GetAreas(bool location, int? cityId, int? labId)
        {

            var areas = location ?
                await _areaService.getAllAreasWithHome(cityId, labId) :
                await _areaService.getAllAreasWithOutHome(cityId, labId);



            var areaModels = _mapper.Map<List<Area>, List<AreaModel>>(areas.ToList());

            return new JsonResult(areaModels);
        }




        public async Task<ActionResult> GetTests(int? labId)
        {

            var tests = await _labService.getLabTestByLabId(labId.Value);



            var areaModels = _mapper.Map<List<LabTest>, List<LabTestSelectModel>>(tests.ToList());

            return new JsonResult(areaModels);
        }

        public async Task<ActionResult> GetBranches(int? cityId, int? labId, int? areaId)
        {

            var branchs = await _branchService.getAllBranchsQuerable(cityId, labId, areaId);

            var areaModels = _mapper.Map<List<Branch>, List<BranchModel>>(branchs);

            return new JsonResult(areaModels);
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
