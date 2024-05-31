using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<Customer> Authenticate(string userName, string password);
        void Register(Customer cust);
        Task<bool> CustAlreadyExists(string userName);
    }
}
