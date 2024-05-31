using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = await _unitOfWork.Customers.GetAllCustomersAsync();
            //var customersDto = customers.Select(s => new CustomerDto
            //{
            //    CustomerId = s.CustomerId,
            //    FirstName = s.FirstName,
            //    LastName = s.LastName,
            //    Email = s.Email,
            //    Phone = s.Phone,
            //    Address = s.Address,
            //})
            //.ToList();
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersDto;
        }

        // Other service methods
        public async Task<CustomerDto> GetCustomerById(int id)
        {
            var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }
        public void AddCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _unitOfWork.Customers.AddCustomerAsync(customer);
            _unitOfWork.CompleteAsync();
        }

        public void UpdateCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _unitOfWork.Customers.UpdateCustomerAsync(customer);
            _unitOfWork.CompleteAsync();
        }

        public void DeleteCustomer(int id)
        {
            _unitOfWork.Customers.DeleteCustomerAsync(id);
            _unitOfWork.CompleteAsync();
        }

       
    }
}
