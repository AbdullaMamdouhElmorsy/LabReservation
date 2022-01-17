using AutoMapper;
using LabReservation.Data.Entities;
using LabReservation.UI.Areas.Admin.Models;
using LabReservation.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<City, CityModel>();
            CreateMap<CityModel,City>();

            CreateMap<Area, AreaModel>()
                .ForMember(d => d.CityName, o => o.MapFrom(s => s.City.Name))
                .ForMember(d => d.LabName, o => o.MapFrom(s => s.Lab.Name))
                .ForMember(d => d.AtHome, o => o.MapFrom(s => s.IsAtHome.Value?"yes":"No"));
            CreateMap<AreaModel, Area>();

            CreateMap<Lab, LabModel>();
            CreateMap<LabModel, Lab>();

            CreateMap<Branch, BranchModel>()
                .ForMember(d => d.LabName, o => o.MapFrom(s => s.Lab.Name))
                .ForMember(d => d.AreaName, o => o.MapFrom(s => s.Area.Name));
            CreateMap<BranchModel, Branch>();

            CreateMap<Test, TestModel>();
            CreateMap<TestModel, Test>();

            CreateMap<Reservation, ReservationModel>();
            CreateMap<ReservationModel, Reservation>();


            CreateMap<LabTestSelectModel, ReservationDetail>();


            CreateMap<LabTestSelectModel, LabTest>();
            CreateMap<LabTest, LabTestSelectModel>()
                .ForMember(d => d.LabName, o => o.MapFrom(s => s.Lab.Name))
                .ForMember(d => d.TestName, o => o.MapFrom(s => s.Test.Name));


        }
    }
}
