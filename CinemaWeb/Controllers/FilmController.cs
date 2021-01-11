using Application;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;

        public FilmController(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FilmDto>>> GetAll()
        {
            var filmList = await _filmService.GetAllAsync();
            var filmModels = _mapper.Map<List<FilmDto>>(filmList);
            return filmModels;
        }
    }
}
