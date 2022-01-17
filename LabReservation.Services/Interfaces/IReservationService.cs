using LabReservation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabReservation.Services.Interfaces
{
    public interface IReservationService
    {
        public int addReservation(Reservation reservations);

        public Task<List<Reservation>> getAllReservation();
        public  Task<bool> addReservationDetails(List<ReservationDetail> reservationDetailsList);
    }
}
