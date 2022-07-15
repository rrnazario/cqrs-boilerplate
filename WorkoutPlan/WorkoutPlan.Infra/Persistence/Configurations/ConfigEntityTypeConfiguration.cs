using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutPlan.Domain.AggregatesModel.AthleteAggregate;
using WorkoutPlan.Infra.Persistence;

namespace TelegramPartHook.Infrastructure.Persistence.EFConfig
{
    internal class ConfigEntityTypeConfiguration
        : IEntityTypeConfiguration<Athlete>
    {
        public void Configure(EntityTypeBuilder<Athlete> builder)
        {
            builder.ToTable("athlete", WorkoutContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();
        }
    }
}
