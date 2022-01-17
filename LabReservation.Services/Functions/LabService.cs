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
    public class LabService : ILabService
    {
        public async Task<List<Lab>> getAllLabs()
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Labs.OrderBy(e => e.Name).ToListAsync();
            }
        }

        public async Task<Lab> addLab(Lab lab)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Labs.Add(lab);
                await db.SaveChangesAsync();
                return lab;
            }
        }

        public async Task<Lab> getLabById(int id)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Labs.Include(e => e.LabTests).ThenInclude(x => x.Test).FirstOrDefaultAsync(e => e.LabId == id);
            }
        }
        

        public async Task<Lab> updateLab(Lab lab)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Labs.Update(lab);
                await db.SaveChangesAsync();
                return lab;
            }
        }


        public async Task<bool> removeLab(Lab lab)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Labs.Remove(lab);
                await db.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> removeLAbTest(LabTest labTest)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.LabTests.Remove(labTest);
                await db.SaveChangesAsync();
                return true;
            }
        }
        
        public async Task<bool> addRange(List<LabTest> labTestList)
        {
            LabReservationContext db = new LabReservationContext();
            
                db.LabTests.AddRange(labTestList);
                await db.SaveChangesAsync();
                return true;
            
        }
        public async Task<bool> removeRange(List<LabTest> labTestList)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.LabTests.RemoveRange(labTestList);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> updateRange(List<LabTest> originalUserProfile , List<LabTest> userProfileViewModel)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Entry(originalUserProfile).CurrentValues.SetValues(userProfileViewModel);
                //db.LabTests.UpdateRange(labTestList);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IReadOnlyList<LabTest>> getAllLabTests()
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.LabTests.Include(e => e.Test).Include(e => e.Lab).ToListAsync();
            }
        }

        public async Task<List<LabTest>> getLabTestById(int id)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.LabTests.Where(e => e.LabId == id).ToListAsync();
            }
        }

        public async Task<LabTest> getLabTestByLabIdAndTestId(int? TestId, int? LabId)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.LabTests.Where(e => e.LabId == LabId && e.TestId == TestId).FirstOrDefaultAsync();
            }
        }


        public async Task<List<LabTest>> getLabTestByLabId(int LabId)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.LabTests.Where(e => e.LabId == LabId).ToListAsync();
            }
        }




    }
}
