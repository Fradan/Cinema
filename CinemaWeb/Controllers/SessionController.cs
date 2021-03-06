﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Application;
using AutoMapper;

namespace CinemaWeb.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<int> Add([FromBody] SessionDto sessionDto)
        {
            var session = _mapper.Map<Session>(sessionDto);
            return await _sessionService.AddSessionAsync(session);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task Update(int id, [FromBody] SessionDto sessionDto)
        {
            var session = _mapper.Map<Session>(sessionDto);
            await _sessionService.UpdateSessionAsync(id, session);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            await _sessionService.DeleteSessionAsync(id);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<SessionDto>> GetAsync(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            return _mapper.Map<SessionDto>(session);
        }

        [Route("bydate={date}")]
        [HttpGet]
        public async Task<ActionResult<List<SessionDto>>> GetSessionsByDate(DateTime date)
        {
            var sessions = await _sessionService.FindByDateAsync(date);
            return _mapper.Map<List<SessionDto>>(sessions);
        }
    }
}
