using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.CQRS.Commands
{
    public record CreateCustomerCommand(string FirstName, string LastName, string Email, string Phone, string Address) : IRequest<int>;

    public record UpdateCustomerCommand(int CustomerId, string FirstName, string LastName, string Email, string Phone, string Address) : IRequest<Unit>;

    public record DeleteCustomerCommand(int CustomerId) : IRequest<Unit>;
}
