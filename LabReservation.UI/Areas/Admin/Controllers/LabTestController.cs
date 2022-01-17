using AutoMapper;
using LabReservation.Data.Entities;
using LabReservation.Services.Interfaces;
using LabReservation.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Areas.Admin.Controllers
{
    public class LabTestController : Controller
    {
        private readonly ITestService _testService;
        private readonly ILabService _labService;
        private readonly IMapper _mapper;

        public LabTestController(ITestService testService,
            IMapper mapper,
            ILabService labService)
        {
            _testService = testService;
            _labService = labService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var labtest = await _labService.getAllLabTests();
            var data = _mapper.Map<IReadOnlyList<LabTest>, IReadOnlyList<LabTestSelectModel>>(labtest);
            return View(data);
        }


        // GET: LabTestController/Create
        public async Task<IActionResult> Create(int? LabId)
        {
            var labTestModel = new LabTestModel();
            var tests = await _testService.getAllTests();
            var lab = await _labService.getLabById(LabId.Value);
            labTestModel.LabId = lab.LabId;
            var testcopy = new List<Test>();
            testcopy.AddRange(tests);

            if (lab.LabTests.Count >= 1)
                labTestModel.IsAdd = false;


            foreach (var item in lab.LabTests)
            {
                foreach (var test in testcopy)
                {
                    if (item.TestId == test.TestId)
                        tests.Remove(test);
                }
            }


            labTestModel.AllTests = tests.ToList();



            labTestModel.Lab = lab;

            var x = lab.LabTests.Select(o => new LabTestSelectModel
            {
                Fees = o.Fees,
                LabId = o.LabId,
                TestId = o.TestId,
                TestName = o.Test.Name,
                Selected = true
            }).ToList();

            labTestModel.SelectedTests.AddRange(x);

            var y = tests.Select(o => new LabTestSelectModel
            {
                LabId = lab.LabId,
                TestId = o.TestId,
                TestName = o.Name,
                Selected = false
            }).ToList();
            labTestModel.SelectedTests.AddRange(y);

            return View(labTestModel);
        }

        // POST: LabTestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LabTestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var data = _mapper.Map<List<LabTestSelectModel>, List<LabTest>>(model.SelectedTests.Where(e => e.Selected).ToList());
             
                var originalLAbTest = await _labService.getLabTestById(model.LabId);

                if (model.IsAdd)
                {
                    await _labService.addRange(data);
                }
                else
                {
                  
                    var listToAdd = data.Except(originalLAbTest).ToList();
                    var listRoRemove = originalLAbTest.Except(data).ToList();
                    await _labService.removeRange(listRoRemove);
                    await _labService.addRange(listToAdd);

                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(model);
            }


        }

        public async Task<IActionResult> Delete(int? TestId, int? LabId)
        {
            if (TestId == null || LabId == null)
            {
                return NotFound();
            }

            var test = await _labService.getLabTestByLabIdAndTestId(TestId , LabId);
            var model = _mapper.Map<LabTest, LabTestSelectModel>(test);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(LabTestSelectModel model)
        {
            var labTest = await _labService.getLabTestByLabIdAndTestId(model.TestId , model.LabId);

            if (await _labService.removeLAbTest(labTest))
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
