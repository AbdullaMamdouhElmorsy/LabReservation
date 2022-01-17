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
    public class BranchsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBranchService _branchService;
        private readonly IAreaService _areaService;
        private readonly ILabService _labService;

        public BranchsController(IMapper mapper,
            IBranchService branchService,
            IAreaService areaService,
            ILabService labService)
        {
            _mapper = mapper;
            _branchService = branchService;
            _areaService = areaService;
            _labService = labService;
        }
        public async Task<IActionResult> Index()
        {
            var branchs = await _branchService.getAllBranchs();
            var data = _mapper.Map<IReadOnlyList<Branch>, IReadOnlyList<BranchModel>>(branchs);
            return View(data);
        }


        public async Task<IActionResult> Create()
        {
            var branch = new BranchModel();
            var areas = await _areaService.getAllAreas();
            var labs = await _labService.getAllLabs();

            var areasData = _mapper.Map<List<Area>, List<AreaModel>>(areas.ToList());
            var labsData = _mapper.Map<List<Lab>, List<LabModel>>(labs.ToList());
            
            branch.Areas = areasData;
            branch.Labs = labsData;
            return View(branch);
        }

        // POST: BranchsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BranchModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<BranchModel, Branch>(model);
                await _branchService.addBranch(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: BranchsController/Edit/5
        public async Task<IActionResult> Edit(int BranchId)
        {
            var branch = await _branchService.getBranchById(BranchId);

            var areas = await _areaService.getAllAreas();
            var labs = await _labService.getAllLabs();

            var areasData = _mapper.Map<List<Area>, List<AreaModel>>(areas.ToList());
            var labsData = _mapper.Map<List<Lab>, List<LabModel>>(labs.ToList());

            var model = _mapper.Map<Branch, BranchModel>(branch);

            model.Areas = areasData;
            model.Labs = labsData;

      
       
            return View(model);
        }

        // POST: BranchsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int BranchId, BranchModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<BranchModel, Branch>(model);
                await _branchService.updateBranch(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: BranchsController/Delete/5
        public async Task<IActionResult> Delete(int? BranchId)
        {
            if (BranchId == null)
            {
                return NotFound();
            }
            var branch = await _branchService.getBranchById(BranchId.Value);
            var model = _mapper.Map<Branch, BranchModel>(branch);
            return View(model);
        }

        // POST: BranchsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BranchModel model)
        {
            var branch = await _branchService.getBranchById(model.BranchId);

            if (await _branchService.removeBranch(branch))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Delete", new { CityId = model.BranchId });
            }
        }

        public async Task<ActionResult> GetAreas(int labId)
        {
            var areas = await _areaService.getAreasByLabId(labId);

            var areaModels = _mapper.Map<List<Area>, List<AreaModel>>(areas);

            return new JsonResult(areaModels);
        }
    }
}
