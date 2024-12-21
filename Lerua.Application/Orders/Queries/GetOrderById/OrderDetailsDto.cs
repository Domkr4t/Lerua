using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.Orders.Queries.GetOrderById
{
    public class OrderDetailsDto : IMapWith<Order>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDetailsDto>()
                .ForMember(dto => dto.Items,
                           opt => opt.MapFrom(src => src.OrderItems));
        }
    }

    public class OrderItemDto : IMapWith<OrderItem>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
