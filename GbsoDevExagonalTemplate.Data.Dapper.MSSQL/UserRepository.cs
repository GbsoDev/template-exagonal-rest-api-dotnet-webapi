using Dapper;
using GbsoDevExagonalTemplate.Domain.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace GbsoDevExagonalTemplate.Data.Dapper.MSSQL
{
	public abstract class UserRepository : Repository, IUserRepository
	{
		private readonly string connectionString = "Data Souce=.;Initial Catalog=exagonal_app_db;Integrated Security=true";
		public UserRepository()
		{
		}

		public async Task<User> AddUserAsync(User user)
		{
			var result = await AddAsync(user);
			await SaveChangesAsync();
			return result;
		}

		public async Task<User> AddAsync(User entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var query = @"INSERT INTO Users 
						(Name, UserName, Password, Enabled, CreatedDate) 
						VALUES
						(@Name, @UserName, @Password, @Enabled); 
						SELECT LAST_INSERT_ID()";
			int id;
			using (var connection = new SqlConnection(connectionString))
			{
				id = await connection.QuerySingleAsync<int>(query, entity);
			}
			var result = await GetByIdAsync(id, cancellationToken);
			return result!;
		}

		public async Task<User[]> ListAsync(CancellationToken cancellationToken = default)
		{
			var query = @"SELECT 
						Id, Name, UserName, Password, Enabled, CreatedDate 
						FROM Users";
			IEnumerable<User> result;
			using (var connection = new SqlConnection(connectionString))
			{
				result = await connection.QueryAsync<User>(query);
			}
			return result.ToArray();
		}

		public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			if(id <= 0) throw new ArgumentNullException(nameof(id));
			var query = @"SELECT 
						Id, Name, UserName, Password, Enabled, CreatedDate 
						FROM Users
						WHERE Id = @Id";
			User result;
			using (var connection = new SqlConnection(connectionString))
			{
				result = await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });	
			}
			return result;
		}

		public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var entityResult = await GetByIdAsync(entity.Id, cancellationToken);
			if (entityResult == null) throw new ApplicationException("Está intentando actualizar un registro que no existe");

			var query = @"UPDATE Users 
						SET
						Name = @Name, 
						UserName = @UserName, 
						Password = @Password, 
						Enabled = @Enabled";
			if (typeof(User).GetInterface(nameof(IAuditableEntity<int>)) != null)
			{
				((IAuditableEntity<int>)entity).UpdatedDate = DateTime.Now;
				query += ",UpdatedDate = @UpdatedDate";
			}
			query += "WHERE Id = @Id";

			using (var connection = new SqlConnection(connectionString))
			{
				await connection.QueryAsync(query, entity);
			}
			var result = await GetByIdAsync(entity.Id, cancellationToken);
			return result!;
		}

		public async Task<User> UpdateAsync(User entity, Expression<Func<User, object>> @object, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var entityResult = await GetByIdAsync(entity.Id, cancellationToken);
			if (entityResult == null) throw new ApplicationException("Está intentando actualizar un registro que no existe");
			var objectResult = @object?.Compile()?.Invoke(entity);
			if (objectResult == null) throw new ArgumentNullException(nameof(@object));
			var propperties = objectResult.GetType().GetProperties();
			objectResult = AddPropery(objectResult, "Id", entity.Id);
			var setters = propperties.Select(x => $"{x.Name} = @{x.Name}");
			var query = $@"UPDATE Users 
						SET {string.Join(",", setters)}
						WHERE Id = @Id";
			using (var connection = new SqlConnection(connectionString))
			{
				await connection.QueryAsync(query, objectResult);
			}
			var result = await GetByIdAsync(entity.Id, cancellationToken);
			return result!;
		}

		public async Task DeleteAsync(User entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var query = @"DELETE 
						FROM Users
						WHERE Id = @Id";
			int result;
			using (var connection = new SqlConnection(connectionString))
			{
				result = await connection.ExecuteAsync(query, new { entity.Id });
			}
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await Task.FromResult(0);
		}

		public Task<bool> ValidateUserAsync(string userName, string encript, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public async Task EnableAsync(int id, CancellationToken cancellationToken = default)
		{
			await ChageStateAsync(id, true, cancellationToken);
		}

		public async Task DisableAsync(int id, CancellationToken cancellationToken = default)
		{
			await ChageStateAsync(id, false, cancellationToken);
		}

		private async Task ChageStateAsync(int id, bool state, CancellationToken cancellationToken = default)
		{
			if (id <= 0) throw new ArgumentNullException(nameof(id), "El id no puede ser 0");
			var entityResult = await GetByIdAsync(id, cancellationToken);
			if (entityResult == null) throw new ApplicationException("Está intentando actualizar un registro que no existe");
			var query = @"UPDATE Users SET Enabled = @Enabled";
			var @params = (object)new
			{
				Id = id,
				Enabled = state,
			};
			if (typeof(User).GetInterface(nameof(IAuditableEntity<int>)) != null)
			{
				@params = AddPropery(@params, nameof(User.CreatedDate), DateTime.Now);
				query += ",UpdatedDate = @UpdatedDate";
			}
			query += "WHERE Id = @Id";
			using (var connection = new SqlConnection(connectionString))
			{
				await connection.QueryAsync(query, @params);
			}
		}
	}
}
