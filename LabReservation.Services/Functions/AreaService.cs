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
    public class AreaService : IAreaService
    {
            
        public async Task<List<Area>> getAllAreas()
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Areas.Include(e => e.City).Include(e => e.Lab).OrderBy(e => e.Name).ToListAsync();
            }
        }


        public async Task<List<Area>> getAllAreasWithHome(int? cityId , int? labId)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                var list = db.Areas.Include(e => e.City).Where(e => e.IsAtHome.Value);
                if (cityId.HasValue)
                {
                    list = list.Where(e => e.CityId == cityId.Value);
                }


                if (labId.HasValue)
                {
                    list = list.Where(e => e.LabId == labId.Value);
                }



                return await list.ToListAsync();
            }
        }
        
        public async Task<List<Area>> getAllAreasWithOutHome(int ? cityId, int ? labId)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
             
                var list = db.Areas.Include(e => e.City).AsQueryable();
                if (cityId.HasValue)
                {
                    list = list.Where(e => e.CityId == cityId.Value);
                }


                if (labId.HasValue)
                {
                    list = list.Where(e => e.LabId == labId.Value);
                }



                return await list.ToListAsync();
            }
        }

        public async Task<Area> addArea(Area area)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Areas.Add(area);
                await db.SaveChangesAsync();
                return area;
            }
        }

        public async Task<Area> getAreaById(int id)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Areas.FindAsync(id);
            }
        }
           public async Task<List<Area>> getAreasByLabId(int id)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Areas.Where(e => e.LabId == id).ToListAsync();
            }
        }


        public async Task<Area> updateArea(Area area)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Areas.Update(area);
                await db.SaveChangesAsync();
                return area;
            }
        }


        public async Task<bool> removeArea(Area area)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Areas.Remove(area);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
