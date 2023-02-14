using AuthService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.DataAccess.Configurations
{
    /// <summary>
    /// User configuration
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration&lt;AuthService.BusinessLogic.Models.User&gt;" />
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Email).IsRequired();
            builder.HasAlternateKey(x => x.Email);

            builder.Property(x => x.PasswordHash).IsRequired();

            builder.Property(x => x.LockoutEndAtUtc).IsRequired(false);

            builder.Property(x => x.LockoutEnabled).IsRequired();

            builder.Property(x => x.AccessFailedCount).IsRequired();

            builder.HasCheckConstraint("CC_AccessFailedCount_GTE_0", "\"AccessFailedCount\" >= 0");

            builder.Property(x => x.CreatedAtUtc).IsRequired();

            builder.Property(x => x.UpdatedAtUtc).IsRequired(false);

            builder.ToTable("Users");
        }
    }
}
