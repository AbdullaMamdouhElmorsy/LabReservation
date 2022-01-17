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
    public class BranchService : IBranchService
    {
        public async Task<List<Branch>> getAllBranchs()
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Branchs.Include(e => e.Area).Include(e => e.Lab).OrderBy(e => e.Name).ToListAsync();
            }
        }

        public async Task<List<Branch>> getAllBranchsQuerable(int? cityId, int? labId , int? areaId)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                var branchs = db.Branchs.Include(e => e.Area).AsQueryable();

                if (cityId.HasValue)
                {
                    branchs = branchs.Where(e => e.Area.CityId == cityId.Value);
                }

                if (labId.HasValue)
                {
                    branchs = branchs.Where(e => e.LabId == labId.Value);
                }

                if (areaId.HasValue)
                {
                    branchs = branchs.Where(e => e.AreaId == areaId.Value);
                }

                return await branchs.ToListAsync();
            }

        }


        public async Task<List<Branch>> getAllBranchsWithHome()
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Branchs.Include(e => e.Area).OrderBy(e => e.Name).Where(e => e.Area.IsAtHome.Value).ToListAsync();
            }
        }

        public async Task<List<Branch>> getAllBranchsWithOutHome()
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Branchs.Include(e => e.Area).OrderBy(e => e.Name).Where(e => !e.Area.IsAtHome.Value).ToListAsync();
            }

        }

        public async Task<Branch> addBranch(Branch branch)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Branchs.Add(branch);
                await db.SaveChangesAsync();
                return branch;
            }
        }

        public async Task<Branch> getBranchById(int id)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Branchs.FindAsync(id);
            }
        }


        public async Task<Branch> updateBranch(Branch branch)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Branchs.Update(branch);
                await db.SaveChangesAsync();
                return branch;
            }
        }


        public async Task<bool> removeBranch(Branch branch)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Branchs.Remove(branch);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
