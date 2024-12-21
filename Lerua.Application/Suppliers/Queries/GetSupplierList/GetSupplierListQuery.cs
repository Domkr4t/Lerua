using MediatR;
using System.Collections.Generic;

namespace Lerua.Application.Suppliers.Queries.GetSupplierList
{
    public class GetSupplierListQuery : IRequest<List<SupplierLookupDto>>
    {
    }
}
