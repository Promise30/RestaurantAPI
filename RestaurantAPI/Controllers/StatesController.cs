using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;

namespace RestaurantAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public StatesController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet("States")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStates()
        {
            var states = await _dbContext.States.ToListAsync();
            if (states == null || states.Count == 0)
            {
                return NotFound();
            }
            var results = _mapper.Map<List<StateDTO>>(states);
            return Ok(results);
        }
        [HttpGet("StatesWithLGA")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStatesWithLocalGovts()
        {
            //var states = await _dbContext.States.Select(state => new
            //{
            //    state.StateId,
            //    state.Name,
            //    LocalGovernments = state.LocalGovernments.Select(lg => new
            //    {

            //        lg.Name
            //    }).ToList()

            //}).ToListAsync();
            var statesWithLocalGovernments = await _dbContext.States
                    .Select(state => new
                    {
                        state.StateId,
                        state.Name,
                        LocalGovernments = state.LocalGovernments.Select(localGov => localGov.Name).ToList()
                    })
                    .ToListAsync();
            if (statesWithLocalGovernments == null || statesWithLocalGovernments.Count == 0)
            {
                return NotFound();
            }

            return Ok(statesWithLocalGovernments);
        }

    }
}
