using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Customer> Authenticate(string userName, string password);
        void Register(string userName, string password);
        Task<bool> CustAlreadyExists(string userName);
    }
}
