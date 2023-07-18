using GbsoDevExagonalTemplate.Domain.Entities;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Outputs
{
	public interface IUserOutputPort : IEntityOutputPort<User, int>
	{
		bool ValidateUser(string userName, string encript);
		void Enable(int id);
		void Disable(int id);
	}
}
