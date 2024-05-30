using CMS.Application.DTOs;
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
        Task<RegisterResDto> Register(RegisterReqDto registerReqDto);
        Task<bool> CustAlreadyExists(string userName);
    }
}
