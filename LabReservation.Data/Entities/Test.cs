using System;
using System.Collections.Generic;

#nullable disable

namespace LabReservation.Data.Entities
{
    public partial class Test
    {
        public Test()
        {
            LabTests = new HashSet<LabTest>();
        }

        public int TestId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LabTest> LabTests { get; set; }
    }
}
