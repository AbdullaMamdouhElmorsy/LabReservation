using System;
using System.Collections.Generic;

#nullable disable

namespace LabReservation.Data.Entities
{
    public partial class ReservationDetail
    {
        public int ReservationDetailId { get; set; }
        public int ReservationId { get; set; }
        public int LabTestId { get; set; }

        public virtual LabTest LabTest { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
