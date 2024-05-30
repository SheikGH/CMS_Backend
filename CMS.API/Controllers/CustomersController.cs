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
            var customer = await _customerService.GetCustomerByIdAsync(id);
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
            var customers = await _customerService.GetCustomersAsync();
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
            var newCustomer = await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.CustomerId }, newCustomer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDto customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            var updatedCustomer = await _customerService.UpdateCustomerAsync(customer);
            if (updatedCustomer == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deletedCustomer = await _customerService.DeleteCustomerAsync(id);
            if (deletedCustomer == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
