using LabReservation.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Areas.Admin.Models
{
    public class LabTestModel
    {
        public Lab Lab { get; set; }

        public int LabId { get; set; }
        public IEnumerable<Test> AllTests { get; set; }

        private List<LabTestSelectModel> _selectedTests;
        public bool IsAdd { get; set; }
        public List<LabTestSelectModel> SelectedTests
        {
            get
            {
                if (_selectedTests == null)
                {
                    _selectedTests = new List<LabTestSelectModel>();
                }
                return _selectedTests;
            }
            set { _selectedTests = value; }
        }
    }
}
