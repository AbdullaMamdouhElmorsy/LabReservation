using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Areas.Admin.Models
{
    public class BranchModel
    {
        public int BranchId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
        public string LabName { get; set; }
        public string AreaName { get; set; }
        [Required]
        public int LabId { get; set; }
        public List<LabModel> Labs { get; set; } = new List<LabModel>();
        [Required]
        public int AreaId { get; set; }
        public List<AreaModel> Areas { get; set; } = new List<AreaModel>();
    }
}
