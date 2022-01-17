using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Areas.Admin.Models
{
    public class LabTestSelectModel
    {
        public string TestName { get; set; }
        public string LabName { get; set; }
        public int LabId { get; set; }
        public int ReservationId { get; set; }
        public int TestId { get; set; }
        public int LabTestId { get; set; }
        public bool Selected { get; set; }
        public decimal Fees { get; set; }
    }
}
