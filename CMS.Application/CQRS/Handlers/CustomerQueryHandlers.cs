using AutoMapper;
using CMS.Application.CQRS.Queries;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.CQRS.Handlers
{
    public class CustomerQueryHandlers :
    IRequestHandler<GetCustomerByIdQuery, Customer>,
    IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerQueryHandlers(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Customers.GetCustomerByIdAsync(request.CustomerId);
            //return await _context.Customers.FindAsync(request.CustomerId);
        }

        public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            //return await await await _unitOfWork.Customers.GetAllCustomersAsync();
            //return await _context.Customers.ToListAsync(cancellationToken);
            return null;
        }
    }
}
