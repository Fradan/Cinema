using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CinemasApi.Controllers
{
    [ApiController]
    [Route("cinema")]
    public class CinemaController : Controller
    {
        private readonly ICinemaService _cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cinemaService.GetAllAsync();
            return Json(result);
        }

        [Route("get")]
        public async Task GetCinema(int id)
        {
            var cinema = await _cinemaService.GetByIdAsync(id);
        }
    }
}
