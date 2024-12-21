using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.Customers.Queries.GetCustomerById
{
    public class CustomerDetailsDto : IMapWith<Customer>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerDetailsDto>();
        }
    }
}
