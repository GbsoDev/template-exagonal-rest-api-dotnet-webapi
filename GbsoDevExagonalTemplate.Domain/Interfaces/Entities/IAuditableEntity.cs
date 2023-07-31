namespace GbsoDevExagonalTemplate.Domain.Interfaces.Entities
{
	public interface IAuditableEntity<TId> : IEntity<TId>
	{
		DateTime CreatedDate { get; set; }
		DateTime? UpdatedDate { get; set; }
	}
}
