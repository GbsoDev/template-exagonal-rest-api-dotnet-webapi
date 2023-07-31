using GbsoDevExagonalTemplate.Domain.Entities;

namespace GbsoDevExagonalTemplate.Dtos.MapperConfigurations
{
	public class UserMapperConfiguration : AutoMapper.Profile
	{
		public UserMapperConfiguration()
		{
			CreateMap<UserDto, User>()
				.ForMember(x => x.Name, m => m.MapFrom(y => y.Name))
				.ForMember(x => x.UserName, m => m.MapFrom(y => y.UserName))
				.ForMember(x => x.Password, m => m.MapFrom(y => y.Password))
				.ForMember(x => x.Enabled, m => m.Ignore())
				.ReverseMap()
				.ForMember(x => x.Password, m => m.Ignore());
		}
	}
}
