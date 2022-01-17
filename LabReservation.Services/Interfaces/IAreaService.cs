using LabReservation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabReservation.Services.Interfaces
{
   public interface IAreaService
    {
        public Task<List<Area>> getAllAreas();

        public Task<List<Area>> getAllAreasWithHome(int? cityId, int? labId);

        public Task<List<Area>> getAllAreasWithOutHome(int? cityId, int? labId);
        public Task<Area> addArea(Area area);
        public Task<Area> getAreaById(int id);
        public Task<Area> updateArea(Area area);
        public Task<bool> removeArea(Area area);
        public Task<List<Area>> getAreasByLabId(int id);
    }
}
