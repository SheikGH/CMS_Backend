using CMS.Core.Interfaces;
using CMS.Infrastructure.Data;
using CMS.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private CustomerRepository _customerRepository;
        private AuthRepository _authRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers => _customerRepository ??= new CustomerRepository(_context);
        public IAuthRepository Auth => _authRepository ??= new AuthRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
