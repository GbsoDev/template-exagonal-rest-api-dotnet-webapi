using FluentValidation;
using GbsoDev.TechTest.Library.Bll.ValidationRules;
using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Outputs;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Application
{
	public abstract class EntityUseCase<TEntity, TId, TOutputPort> : UseCase, IEntityInputPort<TEntity, TId>
		where TEntity : class, IEntity<TId>
		where TOutputPort : class, IEntityOutputPort<TEntity, TId>
	{
		protected TOutputPort MainOutputPort => _mainOutputPort.Value;
		private Lazy<TOutputPort> _mainOutputPort;
		protected IValidator<TEntity> MainVr { get; }

		protected EntityUseCase(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_mainOutputPort = ActivatorUtilities.GetServiceOrCreateInstance<Lazy<TOutputPort>>(serviceProvider);
			MainVr = ActivatorUtilities.GetServiceOrCreateInstance<IValidator<TEntity>>(serviceProvider);
		}

		public virtual TEntity Create(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var validate = MainVr.Validate(entity, x => x.IncludeRuleSets(ValidationRuleSets.CREATE, ValidationRuleSets.ALL));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			var registeredEntity = MainOutputPort.Register(entity);
			MainOutputPort.SaveChanges();
			return registeredEntity;
		}

		public virtual TEntity[] Get()
		{
			return MainOutputPort.List();
		}

		public virtual TEntity? GetById(TId id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			return MainOutputPort.GetById(id);
		}

		public virtual TEntity Update(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var validate = MainVr.Validate(entity, x => x.IncludeRuleSets(ValidationRuleSets.UPDATE, ValidationRuleSets.ALL));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			var updatedEntity = MainOutputPort.Update(entity);
			MainOutputPort.SaveChanges();
			return updatedEntity;
		}

		public virtual void Delete(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var validate = MainVr.Validate(entity, x => x.IncludeRuleSets(ValidationRuleSets.DELETE));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			MainOutputPort.Delete(entity);
			MainOutputPort.SaveChanges();
		}
	}
}
