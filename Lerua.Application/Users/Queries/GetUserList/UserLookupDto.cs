using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.Users.Queries.GetUserList
{
    public class UserLookupDto : IMapWith<User>
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dto => dto.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
