using CMS.Application.DTOs;
using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<CustomerDto> AddCustomerAsync(CustomerDto customer);
        Task<CustomerDto> UpdateCustomerAsync(CustomerDto customer);
        Task<CustomerDto> DeleteCustomerAsync(int id);
    }
}
