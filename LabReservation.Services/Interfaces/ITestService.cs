using LabReservation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabReservation.Services.Interfaces
{
    public interface ITestService
    {
        public Task<List<Test>> getAllTests();
        public Task<Test> addTest(Test test);
        public Task<Test> getTestById(int id);
        public Task<Test> updateTest(Test test);
        public Task<bool> removeTest(Test test);

        public Task<List<Test>> getTestByLabId(int id);
    }
}
