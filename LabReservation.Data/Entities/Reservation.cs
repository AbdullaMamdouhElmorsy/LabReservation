using System;
using System.Collections.Generic;

#nullable disable

namespace LabReservation.Data.Entities
{
    public partial class Reservation
    {
        public Reservation()
        {
            ReservationDetails = new HashSet<ReservationDetail>();
        }

        public int ReservationId { get; set; }
        public DateTime ReserveTime { get; set; }
        public decimal TotalAmount { get; set; }
        public bool LocationType { get; set; }
        public string Address { get; set; }
        public int? BranchId { get; set; }
        public int LabId { get; set; }
        public int AreaId { get; set; }

        public virtual Area Area { get; set; }
        public virtual Lab Lab { get; set; }
        public virtual ICollection<ReservationDetail> ReservationDetails { get; set; }
    }
}
