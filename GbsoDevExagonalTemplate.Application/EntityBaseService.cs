using FluentValidation;
using GbsoDevExagonalTemplate.Application.Interfaces;
using GbsoDevExagonalTemplate.Application.ValidationRules;
using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Application
{
	public abstract class EntityBaseService<TEntity, TId, TRepository> : BaseService, IEntityService<TEntity, TId>
		where TEntity : class, IEntity<TId>
		where TRepository : class, IEntityRepository<TEntity, TId>
	{
		protected TRepository MainRepository => _mainRepository.Value;
		private Lazy<TRepository> _mainRepository;
		protected IValidator<TEntity> MainValidationRules { get; }

		protected EntityBaseService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_mainRepository = ActivatorUtilities.GetServiceOrCreateInstance<Lazy<TRepository>>(serviceProvider);
			MainValidationRules = ActivatorUtilities.GetServiceOrCreateInstance<IValidator<TEntity>>(serviceProvider);
		}

		public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var validate = MainValidationRules.Validate(entity, x => x.IncludeRuleSets(ValidationRuleSets.TO_CREATE));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			var result = await MainRepository.AddAsync(entity, cancellationToken);
			await MainRepository.SaveChangesAsync(cancellationToken);
			return result;
		}

		public virtual async Task<TEntity[]> GetAsync(CancellationToken cancellationToken = default)
		{
			var result = await MainRepository.ListAsync(cancellationToken);
			return result;
		}

		public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			var result = await MainRepository.GetByIdAsync(id, cancellationToken);
			return result;
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var validate = MainValidationRules.Validate(entity, x => x.IncludeRuleSets(ValidationRuleSets.TO_UPDATE, ValidationRuleSets.ALL_EXCEPT_ID));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			var result = await MainRepository.UpdateAsync(entity, cancellationToken);
			await MainRepository.SaveChangesAsync(cancellationToken);
			return result;
		}

		public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			var entity = Activator.CreateInstance<TEntity>();
			entity.Id = id;
			await DeleteAsync(entity);
		}

		public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var validate = MainValidationRules.Validate(entity, x => x.IncludeRuleSets(ValidationRuleSets.TO_DELETE));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			MainRepository.Delete(entity);
			await MainRepository.SaveChangesAsync(cancellationToken);
		}
	}
}
