using System;
using System.Collections.Generic;

#nullable disable

namespace LabReservation.Data.Entities
{
    public partial class Lab
    {
        public Lab()
        {
            Areas = new HashSet<Area>();
            Branches = new HashSet<Branch>();
            LabTests = new HashSet<LabTest>();
            Reservations = new HashSet<Reservation>();
        }

        public int LabId { get; set; }
        public string Name { get; set; }
        public decimal HomeFees { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<LabTest> LabTests { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
