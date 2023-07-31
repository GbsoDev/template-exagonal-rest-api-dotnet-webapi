using GbsoDevExagonalTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GbsoDevExagonalTemplate.Data.EfCore.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			{
				builder.HasKey(x => x.Id);
				builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
				builder.Property(x => x.UserName).IsRequired().HasMaxLength(15);
				builder.HasIndex(x => x.UserName).IsUnique();
				builder.Property(x => x.Password).HasColumnType("CHAR(64)").IsRequired().HasMaxLength(64);
				builder.Property(x => x.Enabled).IsRequired().HasDefaultValue(true);
				builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
			}
		}
	}
}

