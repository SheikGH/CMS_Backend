using CMS.Application.CQRS.Commands;
using CMS.Application.CQRS.Queries;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers
{
    //[Authorize]
    public class CustomersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ICustomerService _customerService;
        
        //public CustomersController(IMediator mediator, ICustomerService customerService)
        //{
        //    _mediator = mediator;
        //    _customerService = customerService;
        //}
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            //var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerResDto = new CustomerResDto
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
            };
            return Ok(customerResDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            //var customers = await _mediator.Send(new GetAllCustomersQuery());
            var customers = await  _customerService.GetAllCustomers();
            var customersResDto = customers.Select(s => new CustomerResDto
            {
                CustomerId = s.CustomerId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
            })
           .ToList();

            return Ok(customersResDto);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomer(CustomerDto customer)
        {
            _customerService.AddCustomer(customer);
            return CreatedAtAction("GetCustomerById", new { id = customer.CustomerId }, customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDto customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            _customerService.UpdateCustomer(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
            return NoContent();
        }
        //[HttpPost]
        //public async Task<ActionResult<int>> CreateCustomer(CreateCustomerCommand command)
        //{
        //    var customerId = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, customerId);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerCommand command)
        //{
        //    if (id != command.CustomerId)
        //    {
        //        return BadRequest();
        //    }

        //    await _mediator.Send(command);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCustomer(int id)
        //{
        //    await _mediator.Send(new DeleteCustomerCommand { Id = id });
        //    return NoContent();
        //}
    }
}
