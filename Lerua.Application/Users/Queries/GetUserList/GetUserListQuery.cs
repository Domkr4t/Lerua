using MediatR;
using System.Collections.Generic;

namespace Lerua.Application.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<List<UserLookupDto>>
    {
    }
}

