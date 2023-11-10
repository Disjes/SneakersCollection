using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakersCollection.Api.ViewModels;
using SneakersCollection.Domain.Interfaces.Repositories;

namespace SneakersCollection.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SneakerController : ControllerBase
    {
        private readonly ISneakerRepository _repository;
        private readonly IMapper _mapper;

        public SneakerController(ISneakerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SneakerViewModel>> GetAllSneakers()
        {
            var sneakers = _repository.GetAll();
            var sneakersViewModel = _mapper.Map<IEnumerable<SneakerViewModel>>(sneakers);
            return Ok(sneakersViewModel);
        }

        [HttpGet("{id}")]
        public ActionResult<SneakerViewModel> GetSneakerById(int id)
        {
            var sneaker = _repository.GetById(id);
            if (sneaker == null)
            {
                return NotFound(); // Return 404 if not found
            }
            var sneakerViewModel = _mapper.Map<SneakerViewModel>(sneaker);
            return Ok(sneakerViewModel);
        }

        [HttpPost]
        public ActionResult<SneakerViewModel> CreateSneaker([FromBody] SneakerViewModel sneakerViewModel)
        {
            var sneaker = _mapper.Map<Domain.Entities.Sneaker>(sneakerViewModel);
            var createdSneaker = _repository.Create(sneaker);
            var createdSneakerViewModel = _mapper.Map<SneakerViewModel>(createdSneaker);
            return CreatedAtAction("GetSneakerById", new { id = createdSneakerViewModel.Id }, createdSneakerViewModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSneaker(int id, [FromBody] SneakerViewModel sneakerViewModel)
        {
            var existingSneaker = _repository.GetById(id);
            if (existingSneaker == null)
            {
                return NotFound(); // Return 404 if not found
            }
            _mapper.Map(sneakerViewModel, existingSneaker);
            _repository.Update(existingSneaker);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSneaker(int id)
        {
            var existingSneaker = _repository.GetById(id);
            if (existingSneaker == null)
            {
                return NotFound(); // Return 404 if not found
            }
            _repository.Delete(id);
            return NoContent();
        }
    }
}
