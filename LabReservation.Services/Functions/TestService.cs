using LabReservation.Data.DataContext;
using LabReservation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabReservation.Services.Interfaces
{
    public class TestService : ITestService
    {
        public async Task<Test> addTest(Test test)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Tests.Add(test);
                await db.SaveChangesAsync();
                return test;
            }
        }

        public async Task<List<Test>> getAllTests()
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Tests.OrderBy(e => e.Name).ToListAsync();
            }
        }

        public async Task<Test> getTestById(int id)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Tests.FindAsync(id);
            }
        }

        public async Task<List<Test>> getTestByLabId(int id)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Tests.Include(e => e.LabTests).Where(e => e.LabTests.Any(e => e.LabId == id)).ToListAsync();
            }
        }

        public async Task<bool> removeTest(Test test)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Tests.Remove(test);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Test> updateTest(Test test)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Tests.Update(test);
                await db.SaveChangesAsync();
                return test;
            }
        }
    }
}
