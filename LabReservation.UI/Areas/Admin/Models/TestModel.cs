using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Areas.Admin.Models
{
    public class TestModel
    {
        public int TestId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

    }
}
