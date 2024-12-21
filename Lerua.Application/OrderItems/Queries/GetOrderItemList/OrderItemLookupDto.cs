using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.OrderItems.Queries.GetOrderItemList
{
    public class OrderItemLookupDto : IMapWith<OrderItem>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderItem, OrderItemLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dto => dto.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dto => dto.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
