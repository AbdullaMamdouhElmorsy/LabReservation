using LabReservation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabReservation.Services.Interfaces
{
    public interface ILabService
    {
        public Task<List<Lab>> getAllLabs();
        public Task<Lab> addLab(Lab lab);
        public Task<Lab> getLabById(int id);

        public Task<Lab> updateLab(Lab lab);
        public Task<bool> removeLab(Lab lab);
        public Task<bool> addRange(List<LabTest> labTestList);
        public Task<bool> removeRange(List<LabTest> labTestList);

        public Task<bool> removeLAbTest(LabTest labTest);
        public Task<bool> updateRange(List<LabTest> originalUserProfile, List<LabTest> userProfileViewModel);

        public Task<IReadOnlyList<LabTest>> getAllLabTests();

        public Task<List<LabTest>> getLabTestById(int id);

        public Task<LabTest> getLabTestByLabIdAndTestId(int? TestId, int? LabId);

        public Task<List<LabTest>> getLabTestByLabId(int LabId);
    }
}
