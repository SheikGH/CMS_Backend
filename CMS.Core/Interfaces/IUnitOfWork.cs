using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IAuthRepository Auth { get; }
        Task<int> CompleteAsync();
    }
}
