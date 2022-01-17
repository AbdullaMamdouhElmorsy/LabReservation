using System;
using System.Collections.Generic;

#nullable disable

namespace LabReservation.Data.Entities
{
    public partial class Area
    {
        public Area()
        {
            Branches = new HashSet<Branch>();
            Reservations = new HashSet<Reservation>();
        }

        public int AreaId { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public bool? IsAtHome { get; set; }
        public int LabId { get; set; }

        public virtual City City { get; set; }
        public virtual Lab Lab { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
