using AutoMapper;
using CMS.Application.CQRS.Handlers;
using CMS.Application.Mappings;
using CMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly CreateCustomerHandler _handler;

        public CustomerServiceTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerProfile>();
            });

            //_mapper = configurationProvider.CreateMapper();
            //_customerRepository = new InMemoryCustomerRepository();
            //_handler = new CreateCustomerHandler(_customerRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCustomer_ReturnsCustomerId()
        {
            //var command = new CreateCustomerCommand
            //{
            //    FirstName = "John",
            //    LastName = "Doe",
            //    Email = "john.doe@example.com",
            //    Phone = "1234567890",
            //    Address = "123 Street"
            //};

            //var result = await _handler.Handle(command, CancellationToken.None);

            //Assert.True(result > 0);
        }

        // Additional tests
    }
}
