using Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using AutoMapper;
using Entities.DataTransferObjects;
using MySqlConnector;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Training_Assessment.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly EventDbContext _dbContext;
        public EventController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, EventDbContext dbContext)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]

        public IActionResult GetAllEvents([FromQuery] EventParameters eventParameters)
        {

            if (!eventParameters.ValidMonthRange)
            {
                return BadRequest("Max month of event cannot be less than current month");
            }

            var events = _repository.Event.GetAllEvents(eventParameters);

            var metadata = new
            {
                events.TotalCount,
                events.PageSize,
                events.CurrentPage,
                events.HasNext,
                events.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned {events.TotalCount} event from database");


            return Ok(events);


        }

        [HttpGet("{Id}", Name = "EventById")]
        public IActionResult GetEventById(Guid Id)
        {
            try
            {
                var ev = _repository.Event.GetEventById(Id);
                if (ev == null)
                {
                    _logger.LogError($"Event with id: {Id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned event with id: {Id}");
                    EventDto eventsResult = new EventDto();
                    eventsResult = _mapper.Map<EventDto>(ev);
                    return Ok(eventsResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventById action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{Id}/customer")]
        public IActionResult GetEventWithDetails(Guid Id)
        {
            try
            {
                var events = _repository.Event.GetEventWithDetails(Id);
                if (events == null)
                {
                    _logger.LogError($"Event with id: {Id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned event with details for id: {Id}");
                    var eventResult = _mapper.Map<EventDto>(events);
                    return Ok(eventResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody]EventForCreationDto events)
        {
            try
            {
                if (events == null)
                {
                    _logger.LogError("Event object sent from the customer is null");
                    return BadRequest("Event object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid event object sent from the customer");
                    return BadRequest("Invalid model object");
                }

                var eventEntity = _mapper.Map<Event>(events);
                _repository.Event.CreateEvent(eventEntity);
                _repository.Save();
                var createdEvent = _mapper.Map<EventDto>(eventEntity);
                return CreatedAtRoute("eventById", new { Id = createdEvent.Id }, createdEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEvent action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateEvent(Guid Id, [FromBody]EventForUpdateDto events)
        {
            try
            {
                if (events == null)
                {
                    _logger.LogError("Event object sent from the customer is null");
                    return BadRequest("Event object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid event object sent from the customer");
                    return BadRequest("Invalid model object");
                }
                var eventEntity = _repository.Event.GetEventById(Id);
                if (eventEntity == null)
                {
                    _logger.LogError($"Event with id: {Id} hasn't been found in database");
                    return NotFound();
                }

                _mapper.Map(events, eventEntity);

                _repository.Event.UpdateEvent(eventEntity);
                _repository.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEvent action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteEvent(Guid Id)
        {
            try
            {
                var events = _repository.Event.GetEventById(Id);
                if (events == null)
                {
                    _logger.LogError($"Event with id: {Id} hasn't been found in database");
                    return NotFound();
                }

                _repository.Event.DeleteEvent(events);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEvent action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

