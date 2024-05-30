using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CMS.Infrastructure.Repositories.AuthRepository;

namespace CMS.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext dc;
        public AuthRepository(ApplicationDbContext dc) { this.dc = dc; }
        public async Task<Customer> Authenticate(string userName, string passwordText)
        {
            var cust = await dc.Customers.FirstOrDefaultAsync(x => x.Email == userName
           && x.Password == passwordText
           );
            if (cust == null)
                return null;
            return cust;
        }

        public async Task<Customer> Register(Customer customer)
        {
            dc.Customers.Add(customer);
            return customer; 
        }

        public async Task<bool> CustAlreadyExists(string userName)
        {
            return await dc.Customers.AnyAsync(x => x.Email == userName);
        }
    }

}
