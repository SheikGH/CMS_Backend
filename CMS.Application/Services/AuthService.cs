using AutoMapper;
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

        public void Register(string userName, string password)
        {
            byte[] passwordHash, passwordKey;
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
           
            Customer user = new Customer();
            user.Email = userName;
            //user.Password = passwordHash;
            //user.PasswordKey = passwordKey;
            user.Password = password;
            user.PasswordKey = password;

            _unitOfWork.Auth.Register(user);
            _unitOfWork.CompleteAsync();
        }

        public async Task<bool> CustAlreadyExists(string userName)
        {
            return await _unitOfWork.Auth.CustAlreadyExists(userName);
        }
    }
}
