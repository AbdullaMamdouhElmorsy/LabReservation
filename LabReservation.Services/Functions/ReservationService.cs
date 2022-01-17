using LabReservation.Data.DataContext;
using LabReservation.Data.Entities;
using LabReservation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabReservation.Services.Functions
{
    public class ReservationService : IReservationService
    {
        public int addReservation(Reservation reservations)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.Reservations.Add(reservations);
                db.SaveChanges();
                return reservations.ReservationId;
            }
        }


        public async Task<List<Reservation>>  getAllReservation()
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                return await db.Reservations.Include(e => e.ReservationDetails).ToListAsync();
            }
        }


        public async Task<bool> addReservationDetails(List<ReservationDetail> reservationDetailsList)
        {
            using (LabReservationContext db = new LabReservationContext())
            {
                db.ReservationDetails.AddRange(reservationDetailsList);
                await db.SaveChangesAsync();
                return true;
            }
        }

    }
}
