using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.Suppliers.Queries.GetSupplierList
{
    public class SupplierLookupDto : IMapWith<Supplier>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Supplier, SupplierLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
