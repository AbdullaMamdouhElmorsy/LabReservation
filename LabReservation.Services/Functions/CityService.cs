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
    public class CityService : ICityService
    {

        public async Task<List<City>> getAllCities()
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Cities.OrderBy(e => e.Name).ToListAsync();
            }
        }

        public async Task<City> addCity(City city)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Cities.Add(city);
                await db.SaveChangesAsync();
                return city;
            }
        }

        public async Task<City> getCityById(int id)
        {
            using (LabReservationContext db = new LabReservationContext())
            {

                return await db.Cities.FindAsync(id);
            }
        }


        public async Task<City> updateCity(City city)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Cities.Update(city);
                await db.SaveChangesAsync();
                return city;
            }
        }


        public async Task<bool> removeCity(City city)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Cities.Remove(city);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
