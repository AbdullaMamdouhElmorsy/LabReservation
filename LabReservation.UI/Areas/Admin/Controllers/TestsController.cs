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
    public class TestsController : Controller
    {
        private readonly ITestService _testService;
        private readonly IMapper _mapper;

        public TestsController(IMapper mapper , ITestService testService)
        {
            _testService = testService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var tests = await _testService.getAllTests();
            var data = _mapper.Map<IReadOnlyList<Test>, IReadOnlyList<TestModel>>(tests);
            return View(data);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<TestModel, Test>(model);
                await _testService.addTest(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }



        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int TestId)
        {
            var test = await _testService.getTestById(TestId);
            var model = _mapper.Map<Test, TestModel>(test);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int TestId, TestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<TestModel, Test>(model);
                await _testService.updateTest(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(int? TestId)
        {
            if (TestId == null)
            {
                return NotFound();
            }
            var test = await _testService.getTestById(TestId.Value);
            var model = _mapper.Map<Test, TestModel>(test);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TestModel model)
        {
            var test = await _testService.getTestById(model.TestId);

            if (await _testService.removeTest(test))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Delete", new { TestId = model.TestId });
            }


        }


    }
}
