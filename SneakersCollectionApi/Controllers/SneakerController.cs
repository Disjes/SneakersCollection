using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakersCollection.Api.ViewModels;
using SneakersCollection.Domain.Exceptions;
using SneakersCollection.Domain.Interfaces.Repositories;
using SneakersCollection.Domain.Interfaces.Services;

namespace SneakersCollection.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SneakerController : ControllerBase
    {
        private readonly ISneakerService _sneakerService;
        private readonly IMapper _mapper;

        public SneakerController(ISneakerService sneakerService, IMapper mapper)
        {
            _sneakerService = sneakerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SneakerViewModel>>> GetAllSneakers()
        {
            var sneakers = await _sneakerService.GetAllSneakers();
            var sneakersViewModel = _mapper.Map<IEnumerable<SneakerViewModel>>(sneakers);
            return Ok(sneakersViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SneakerViewModel>> GetSneakerById(Guid id)
        {
            var sneaker = await _sneakerService.GetSneakerById(id);
            if (sneaker == null)
            {
                return NotFound();
            }
            var sneakerViewModel = _mapper.Map<SneakerViewModel>(sneaker);
            return Ok(sneakerViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<SneakerViewModel>> CreateSneaker([FromBody] SneakerViewModel sneakerViewModel)
        {
            var createdSneaker = new Domain.Entities.Sneaker();
            var sneaker = _mapper.Map<Domain.Entities.Sneaker>(sneakerViewModel);
            try
            {
                createdSneaker = await _sneakerService.AddSneaker(sneaker);
            }
            catch (InvalidSneakerSizeException ex)
            {
                return BadRequest(ex.Message);
            }
            var createdSneakerViewModel = _mapper.Map<SneakerViewModel>(createdSneaker);
            return CreatedAtAction("GetSneakerById", new { id = createdSneakerViewModel.Id }, createdSneakerViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSneaker(Guid id, [FromBody] SneakerViewModel sneakerViewModel)
        {
            var existingSneaker = await _sneakerService.GetSneakerById(id);
            if (existingSneaker == null)
            {
                return NotFound();
            }
            var sneakerToBeUpdated = _mapper.Map(sneakerViewModel, existingSneaker);
            _sneakerService.UpdateSneaker(sneakerToBeUpdated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSneaker(Guid id)
        {
            var existingSneaker = await _sneakerService.GetSneakerById(id);
            if (existingSneaker == null)
            {
                return NotFound();
            }
            _sneakerService.RemoveSneaker(existingSneaker.BrandId, id);
            return NoContent();
        }
    }
}
