using LabReservation.Data.Entities;
using LabReservation.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Models
{
    public class ReservationModel
    {
        public Reservation Reservation { get; set; } = new Reservation();

        public int ReservationId { get; set; }
        public DateTime ReserveTime { get; set; }
        public decimal TotalAmount { get; set; }


        public Lab Lab { get; set; }


        [Required]
        public bool LocationType { get; set; }
        public string Address { get; set; }



        [Required]
        public int AreaId { get; set; }
        public List<AreaModel> Areas { get; set; } = new List<AreaModel>();



        [Required]
        public int BranchId { get; set; }
        public List<BranchModel> Branches { get; set; } = new List<BranchModel>();





        [Required]
        public int CityId { get; set; }
        public List<CityModel> Cities { get; set; } = new List<CityModel>();



        [Required]
        public int LabId { get; set; }
        public List<LabModel> Labs { get; set; } = new List<LabModel>();




        public IEnumerable<SelectListItem> AllLabTest { get; set; }


        private List<LabTestSelectModel> _selectedLabTest;
        public List<LabTestSelectModel> SelectedLabTest
        {
            get
            {
                if (_selectedLabTest == null)
                {
                    _selectedLabTest = new List<LabTestSelectModel>();
                }
                return _selectedLabTest;
            }
            set { _selectedLabTest = value; }
        }



    }
}
