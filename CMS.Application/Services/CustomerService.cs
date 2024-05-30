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
        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var customers = await _unitOfWork.Customers.GetCustomersAsync();
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersDto;
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }
        public async Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _unitOfWork.Customers.AddCustomerAsync(customer);
            await _unitOfWork.CompleteAsync();
            return customerDto;
        }

        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _unitOfWork.Customers.UpdateCustomerAsync(customer);
            await _unitOfWork.CompleteAsync();
            return customerDto;
        }

        public async Task<CustomerDto> DeleteCustomerAsync(int id)
        {
            var customer = await _unitOfWork.Customers.DeleteCustomerAsync(id);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
