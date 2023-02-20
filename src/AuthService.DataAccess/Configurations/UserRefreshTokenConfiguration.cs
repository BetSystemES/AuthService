using AuthService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.DataAccess.Configurations
{
    /// <summary>
    /// User refresh token configuration
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration&lt;AuthService.BusinessLogic.Models.UserRefreshToken&gt;" />
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Token).IsRequired();

            builder.Property(x => x.ExpiresAtUtc).IsRequired();

            builder.Property(x => x.ExpiresAtUtc).IsRequired();

            builder.HasOne(x => x.User)
                    .WithMany(x => x.RefreshTokens)
                    .HasForeignKey(x => x.UserId);

            builder.ToTable("UserRefreshTokens");
        }
    }
}
