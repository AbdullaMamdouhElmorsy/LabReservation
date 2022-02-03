using WebApplication.Data.DataContext;
using WebApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Services.Interfaces
{
    public class AreaService : IAreaService
    {
            
        public async Task<List<Area>> getAllAreas()
        {
            using (WebApplicationContext db = new WebApplicationContext())
            {
                return await db.Areas.Include(e => e.City).OrderBy(e => e.Name).ToListAsync();
            }
        }


        

        public async Task<Area> addArea(Area area)
        {
            using (WebApplicationContext db = new WebApplicationContext())
            {
                db.Areas.Add(area);
                await db.SaveChangesAsync();
                return area;
            }
        }

        public async Task<Area> getAreaById(int id)
        {
            using (WebApplicationContext db = new WebApplicationContext())
            {
                return await db.Areas.FindAsync(id);
            }
        }
       

        public async Task<Area> updateArea(Area area)
        {
            using (WebApplicationContext db = new WebApplicationContext())
            {
                db.Areas.Update(area);
                await db.SaveChangesAsync();
                return area;
            }
        }


        public async Task<bool> removeArea(Area area)
        {
            using (WebApplicationContext db = new WebApplicationContext())
            {
                db.Areas.Remove(area);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
