using WebApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Services.Interfaces
{
   public interface IAreaService
    {
        public Task<List<Area>> getAllAreas();
        public Task<Area> addArea(Area area);
        public Task<Area> getAreaById(int id);
        public Task<Area> updateArea(Area area);
        public Task<bool> removeArea(Area area);
    }
}
