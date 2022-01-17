using System;
using System.Collections.Generic;

#nullable disable

namespace LabReservation.Data.Entities
{
    public partial class Branch
    {
        public int BranchId { get; set; }
        public string Name { get; set; }
        public int LabId { get; set; }
        public int AreaId { get; set; }

        public virtual Area Area { get; set; }
        public virtual Lab Lab { get; set; }
    }
}
