using Castle.Core.Resource;
using CMS.API.Controllers;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Tests.Controllers
{
    public class AuthControllerTests
    {
        private Mock<IAuthService> _mockRepo;
        private readonly AuthController _controller;
        IConfiguration configuration;
        public AuthControllerTests()
        {
            _mockRepo = new Mock<IAuthService>();
            _controller = new AuthController(_mockRepo.Object, configuration);
        }
        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUserIsNull()
        {
            // Arrange
            var loginReq = new LoginReqDto { Username = "john.doe@example.com", Password = "John@123" };
            _mockRepo.Setup(s => s.Authenticate(loginReq.Username, loginReq.Password)).ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.Login(loginReq);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        
    }
}
