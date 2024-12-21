using MediatR;
using System.Collections.Generic;

namespace Lerua.Application.Customers.Queries.GetCustomerList
{
    public class GetCustomerListQuery : IRequest<List<CustomerLookupDto>>
    {
    }
}
