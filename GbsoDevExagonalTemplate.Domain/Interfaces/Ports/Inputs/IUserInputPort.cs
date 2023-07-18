using GbsoDevExagonalTemplate.Domain.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs.Actions;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs
{
	public interface IUserInputPort :
		ICreateInput<User, int>,
		IGetInput<User, int>,
		IUpdateInput<User, int>
	{
		bool ValidateUser(User user);
		void Enable(int id);
		void Desable(int id);
	}
}
