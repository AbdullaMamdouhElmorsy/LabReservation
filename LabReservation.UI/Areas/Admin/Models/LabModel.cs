using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Areas.Admin.Models
{
    public class LabModel
    {
        public int LabId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal HomeFees { get; set; }
    }
}
