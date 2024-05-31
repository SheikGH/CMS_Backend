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
    public class CustomerCommandHandlers :
    IRequestHandler<CreateCustomerCommand, int>,
    IRequestHandler<UpdateCustomerCommand, Unit>,
    IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerCommandHandlers(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            //await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync();
            return customer.CustomerId;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            //_unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync();
            return Unit.Value;
            //var customer = await _context.Customers.FindAsync(request.CustomerId);
            //if (customer == null) throw new NotFoundException(nameof(Customer), request.CustomerId);

            //customer.FirstName = request.FirstName;
            //customer.LastName = request.LastName;
            //customer.Email = request.Email;
            //customer.Phone = request.Phone;
            //customer.Address = request.Address;

            //await _context.SaveChangesAsync(cancellationToken);

            //return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            //await _unitOfWork.Customers.Delete(request.CustomerId);
            //await _unitOfWork.CompleteAsync();
            return Unit.Value;
            //var customer = await _context.Customers.FindAsync(request.CustomerId);
            //if (customer == null) throw new NotFoundException(nameof(Customer), request.CustomerId);

            //_context.Customers.Remove(customer);
            //await _context.SaveChangesAsync(cancellationToken);

            //return Unit.Value;
        }
    }
}
