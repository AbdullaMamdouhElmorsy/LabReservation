using AutoMapper;
using LabReservation.Data.Entities;
using LabReservation.Services.Interfaces;
using LabReservation.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Areas.Admin.Controllers
{
   
    public class AreasController : Controller
    {

        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        private readonly ILabService _labService;

        public AreasController(IAreaService areaService, IMapper mapper , ICityService cityService, ILabService labService)
        {
            _areaService = areaService;
            _mapper = mapper;
            _cityService = cityService;
            _labService = labService;
        }

        // GET: AreasController
        public async Task<IActionResult> Index()
        {
            var areas = await _areaService.getAllAreas();    
            var data = _mapper.Map<IReadOnlyList<Area>, IReadOnlyList<AreaModel>>(areas);
            return View(data);
        }

        // GET: CitiesController/Create
        public async Task<IActionResult> Create()
        {
            var area = new AreaModel();
            var cities = await _cityService.getAllCities();
            var labs = await _labService.getAllLabs();
            var citiesData = _mapper.Map<List<City>, List<CityModel>>(cities.ToList());
            var labsData = _mapper.Map<List<Lab>, List<LabModel>>(labs.ToList());
            area.Citites = citiesData;
            area.LabModels = labsData;
            return View(area);
        }

        // POST: AreasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AreaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<AreaModel, Area>(model);
                await _areaService.addArea(data);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }


        // GET: AreasController/Edit/5
        public async Task<IActionResult> Edit(int AreaId)
        {
          
            var area = await _areaService.getAreaById(AreaId);
            var cities = await _cityService.getAllCities();
            var citiesData = _mapper.Map<List<City>, List<CityModel>>(cities.ToList());
            var model = _mapper.Map<Area, AreaModel>(area);
            model.Citites = citiesData;
            return View(model);
        }


        // POST: AreasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int AreaId, AreaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<AreaModel, Area>(model);
                await _areaService.updateArea(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }




        public async Task<IActionResult> Delete(int? AreaId)
        {
            if (AreaId == null)
            {
                return NotFound();
            }
            var city = await _areaService.getAreaById(AreaId.Value);
            var model = _mapper.Map<Area, AreaModel>(city);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AreaModel model)
        {
            var area = await _areaService.getAreaById(model.AreaId);

            if (await _areaService.removeArea(area))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Delete", new { AreaId = model.AreaId });
            }


        }


    }
}
