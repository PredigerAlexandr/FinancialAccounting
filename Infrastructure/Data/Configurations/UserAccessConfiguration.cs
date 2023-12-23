using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserAccessConfiguration:IEntityTypeConfiguration<UserAccess>
{
    public void Configure(EntityTypeBuilder<UserAccess> builder)
    {
        builder.HasKey(k => k.Id);
        builder.HasIndex(i => i.Id);
    }
}