using CMS.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.CQRS.Queries
{
    public record GetCustomerByIdQuery(int CustomerId) : IRequest<Customer>;

    public record GetAllCustomersQuery : IRequest<IEnumerable<Customer>>;
}
