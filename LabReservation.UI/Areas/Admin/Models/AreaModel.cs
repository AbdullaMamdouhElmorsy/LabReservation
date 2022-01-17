using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Areas.Admin.Models
{
    public class AreaModel
    {
        public int AreaId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        public bool IsAtHome { get; set; }

        public string AtHome { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int LabId { get; set; }
        public string CityName { get; set; }
        public string LabName { get; set; }
        public List<CityModel> Citites { get; set; } = new List<CityModel>();
        public List<LabModel> LabModels { get; set; } = new List<LabModel>();
    }
}
