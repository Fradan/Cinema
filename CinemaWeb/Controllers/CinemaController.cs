using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application;
using AutoMapper;

namespace CinemaWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CinemaController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly IMapper _mapper;

        public CinemaController(ICinemaService cinemaService, IMapper mapper)
        {
            _cinemaService = cinemaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CinemaDto>>> GetAll()
        {
            var cinemaList = await _cinemaService.GetAllAsync();
            var cinemaModels = _mapper.Map<List<CinemaDto>>(cinemaList);
            return cinemaModels;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CinemaDto>> GetCinema(int id)
        {
            var cinema =  await _cinemaService.GetByIdAsync(id);
            var cinemaModel = _mapper.Map<CinemaDto>(cinema);
            return cinemaModel;
        }
    }
}
