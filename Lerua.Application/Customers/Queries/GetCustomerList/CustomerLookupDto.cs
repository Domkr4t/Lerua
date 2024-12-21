using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.Customers.Queries.GetCustomerList
{
    public class CustomerLookupDto : IMapWith<Customer>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
