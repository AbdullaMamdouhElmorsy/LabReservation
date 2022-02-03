using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication.Data.Entities
{
    public partial class Area
    {
     

        public int AreaId { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
