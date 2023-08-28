using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Events;
using Microsoft.AspNetCore.Mvc;

namespace Training_Assessment.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryWrapper _repository;
    
        public CustomerController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;

        }

        [HttpGet("{CustomerId}")]

        public IActionResult GetCustomersByEvent(Guid CustomerId)
        {

                var customers = _repository.Customer.GetCustomersByEvent(CustomerId);
                _logger.LogInfo($"Returned all customer from database");


                return Ok(customers);

        }

        [HttpGet("{CustomerId}/{Id}")]
        public IActionResult GetCustomersByEvent(Guid CustomerId, Guid Id)
        {

                var customers = _repository.Customer.GetCustomersByEvent(CustomerId, Id);
                if (customers.IsEmptyObject())
                {
                    _logger.LogError($"Customer with id: {Id}, hasn't been found in db.");
                    return NotFound();
                }

                    return Ok(customers);
        }

    }
}
