using WebApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Services.Interfaces
{
   public interface ICityService
    {
        public Task<List<City>> getAllCities();
        public Task<City> addCity(City city);
        public Task<City> getCityById(int id);
        public Task<City> updateCity(City city);
        public Task<bool> removeCity(City city);
    }
}
