using AutoMapper;
using LabReservation.Data.Entities;
using LabReservation.Services.Interfaces;
using LabReservation.UI.Areas.Admin.Models;
using LabReservation.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabReservation.UI.Areas.Admin.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IMapper _mapper;

       
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService , IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
          var reservations =  await _reservationService.getAllReservation();



         var reservationsModel = _mapper.Map<List<Reservation> , List<ReservationModel>>(reservations);
            return View(reservationsModel);
        }
    }
}
