﻿using Castle.Core.Resource;
using CMS.API.Controllers;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Tests.Controllers
{
    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _mockRepo;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockRepo = new Mock<ICustomerService>();
            _controller = new CustomersController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetCustomer_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange
            var customerId = 1;
            var customer = new CustomerDto { CustomerId = 10, FirstName = "Ahmed", LastName = "Kasim", Email = "ahmed@example.com", Phone = "1234567890", Address = "UAE" , Password = "Ahmed@123", PasswordKey = "Ahmed@123"};
            //var customer = new CustomerDto { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "1234567890", Address = "123 Main St", Password = "John@123", PasswordKey = "John@123" };
            //_mockRepo.Setup(repo => repo.GetCustomerByIdAsync(customerId)).ReturnsAsync(new CustomerDto());
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(customerId)).ReturnsAsync(new CustomerDto());

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Asserts
            var actionResult = Assert.IsType<ActionResult<CustomerDto>>(result);
            var returnValue = Assert.IsType<CustomerResDto>(actionResult.Value);
            Assert.Equal(customerId, returnValue.CustomerId);
        }

        [Fact]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = 1;
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(customerId)).ReturnsAsync((CustomerDto?)null);

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsCreatedAtAction_WhenCustomerIsCreated()
        {
            // Arrange
            var customer = new CustomerDto { CustomerId = 0, FirstName = "Ahmed", LastName = "Kasim", Email = "ahmed@example.com", Phone = "1234567890", Address = "UAE" , Password = "Ahmed@123", PasswordKey = "Ahmed@123"};
            //var customer = new CustomerDto { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "1234567890", Address = "123 Main St", Password = "John@123", PasswordKey = "John@123" };

            // Act
            var result = await _controller.CreateCustomer(customer);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<CustomerResDto>(actionResult.Value);
            Assert.Equal(customer.FirstName, returnValue.FirstName);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsNoContent_WhenCustomerIsUpdated()
        {
            // Arrange
            var customerId = 1;
            var customer = new CustomerDto { CustomerId = 1, FirstName = "Ahmed", LastName = "Kasim", Email = "ahmed@example.com", Phone = "1234567890", Address = "UAE" , Password = "Ahmed@123", PasswordKey = "Ahmed@123"};
            //var customer = new CustomerDto { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "1234567890", Address = "123 Main St", Password = "John@123", PasswordKey = "John@123" };

            _mockRepo.Setup(repo => repo.UpdateCustomerAsync(customer)).ReturnsAsync(new CustomerDto());

            // Act
            var result = await _controller.UpdateCustomer(customerId, customer);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNoContent_WhenCustomerIsDeleted()
        {
            // Arrange
            var customerId = 1;
            var customer = new CustomerDto { CustomerId = 1, FirstName = "Ahmed", LastName = "Kasim", Email = "ahmed@example.com", Phone = "1234567890", Address = "UAE" , Password = "Ahmed@123", PasswordKey = "Ahmed@123"};
            //var customer = new CustomerDto { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "1234567890", Address = "123 Main St", Password = "John@123", PasswordKey = "John@123" };
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(customerId)).ReturnsAsync(customer);
            _mockRepo.Setup(repo => repo.DeleteCustomerAsync(customerId)).ReturnsAsync(customer);

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = 1;
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(customerId)).ReturnsAsync((CustomerDto)null);

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

}
