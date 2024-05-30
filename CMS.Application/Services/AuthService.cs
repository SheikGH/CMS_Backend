using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Customer> Authenticate(string userName, string passwordText)
        {
            var customer = await _unitOfWork.Auth.Authenticate(userName, passwordText);
            if (customer == null)
                return null;
            //if (!MathPasswordHash(passwordText, customer.Password, customer.PasswordKey))
            //    return null;
            return customer;
        }

        public async Task<RegisterResDto> Register(RegisterReqDto registerReqDto)
        {
            byte[] passwordHash, passwordKey;
            if (!string.IsNullOrWhiteSpace(registerReqDto.Password))
            {
                using (var hmac = new HMACSHA512())
                {
                    passwordKey = hmac.Key;
                    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerReqDto.Password));
                }
            }
            var customer = _mapper.Map<Customer>(registerReqDto);
            customer.PasswordKey = registerReqDto.Password;
            await _unitOfWork.Auth.Register(customer);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<RegisterResDto>(registerReqDto);
        }

        public async Task<bool> CustAlreadyExists(string userName)
        {
            return await _unitOfWork.Auth.CustAlreadyExists(userName);
        }
        private bool MathPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordText));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }
                return true;
            }
        }
    }
}
