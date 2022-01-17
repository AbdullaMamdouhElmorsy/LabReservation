using System;
using System.Collections.Generic;

#nullable disable

namespace LabReservation.Data.Entities
{
    public partial class LabTest
    {
        public LabTest()
        {
            ReservationDetails = new HashSet<ReservationDetail>();
        }

        public int LabId { get; set; }
        public int TestId { get; set; }
        public decimal Fees { get; set; }
        public int LabTestId { get; set; }

        public virtual Lab Lab { get; set; }
        public virtual Test Test { get; set; }
        public virtual ICollection<ReservationDetail> ReservationDetails { get; set; }
    }
}
