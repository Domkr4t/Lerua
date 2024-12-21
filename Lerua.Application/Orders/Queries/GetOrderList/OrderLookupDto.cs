using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Application.Orders.Queries.GetOrderById;
using Lerua.Domain;

namespace Lerua.Application.Orders.Queries.GetOrderList
{
    public class OrderLookupDto : IMapWith<Order>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dto => dto.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dto => dto.Items,
                           opt => opt.MapFrom(src => src.OrderItems));
        }
    }
}
