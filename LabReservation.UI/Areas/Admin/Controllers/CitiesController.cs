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
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CitiesController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var cities = await _cityService.getAllCities();
            var data = _mapper.Map<IReadOnlyList<City>, IReadOnlyList<CityModel>>(cities);
            return View(data);
        }

      
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<CityModel, City>(model);
                await _cityService.addCity(data);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {          
                string message = string.Format("Message: {0}", ex.InnerException.Message);    
                ModelState.AddModelError("ProductSRDate", message);
                return View();
            }

          
        }



        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int CityId)
        {
            var city = await _cityService.getCityById(CityId);
            var model = _mapper.Map<City , CityModel>(city);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int CityId, CityModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<CityModel, City>(model);
                await _cityService.updateCity(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(int? CityId)
        {
            if (CityId == null)
            {
                return NotFound();
            }
            var city = await _cityService.getCityById(CityId.Value);
            var model = _mapper.Map<City ,CityModel>(city);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CityModel model)
        {
            var city = await _cityService.getCityById(model.CityId);
           
            if(await _cityService.removeCity(city))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Delete" , new { CityId = model.CityId });
            }
         
           
        }
    
    
    
    
    }
}
