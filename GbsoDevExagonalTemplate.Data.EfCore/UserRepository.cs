using GbsoDevExagonalTemplate.Domain.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Outputs;

namespace GbsoDevExagonalTemplate.Data.EfCore
{
	public sealed class UserRepository : EntityRepository<User, int>, IUserOutputPort
	{

		public UserRepository(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public void Disable(int id)
		{
			ChageState(id, false);
		}

		public void Enable(int id)
		{
			ChageState(id, true);
		}

		private void ChageState(int id, bool state)
		{
			if (id < 1) throw new ArgumentNullException(nameof(id), "El id no puede ser 0");
			var entryUser = MainContext.Users.Find(id);
			if (entryUser == null) throw new ApplicationException("Está intentando actualizar un registro que no existe");
			entryUser.Enabled = state;
		}

		public bool ValidateUser(string userName, string encript)
		{
			return MainContext.Users.Any(x => x.UserName == userName && x.Enabled == true && x.Password == encript);
		}
	}
}
