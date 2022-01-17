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
    public class LabsController : Controller
    {

        private readonly ICityService _cityService;
        private readonly ILabService _labService;
        private readonly IMapper _mapper;

        public LabsController(ICityService cityService,
            IMapper mapper ,
            ILabService labService)
        {
            _cityService = cityService;
            _labService = labService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var labs = await _labService.getAllLabs();
            var data = _mapper.Map<IReadOnlyList<Lab>, IReadOnlyList<LabModel>>(labs);
            return View(data);
        }


      
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LabModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<LabModel, Lab>(model);
                await _labService.addLab(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }




        // GET: LabsController/Edit/5
        public async Task<IActionResult> Edit(int LabId)
        {
            var lab = await _labService.getLabById(LabId);
            var model = _mapper.Map<Lab, LabModel>(lab);
            return View(model);
        }

        // POST: LabsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int LabId, LabModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<LabModel, Lab>(model);
                await _labService.updateLab(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(int? LabId)
        {
            if (LabId == null)
            {
                return NotFound();
            }
            var city = await _labService.getLabById(LabId.Value);
            var model = _mapper.Map<Lab, LabModel>(city);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(LabModel model)
        {
            var lab = await _labService.getLabById(model.LabId);

            if (await _labService.removeLab(lab))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Delete", new { LabId = model.LabId });
            }


        }


      
        



    }
}
