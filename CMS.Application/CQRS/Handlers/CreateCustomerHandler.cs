using AutoMapper;
using CMS.Application.CQRS.Commands;
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
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            //await _unitOfWork.Customers.AddAsync(customer);
            //await _unitOfWork.CompleteAsync();
            return customer.CustomerId;
        }
    }

    // Other handlers (Update, Delete, GetById, GetAll) go here
}
