﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;
using Microsoft.eShopOnContainers.Services.Ordering.Infrastructure;

namespace Ordering.Infrastructure.EntityConfigurations
{
    class BuyerEntityTypeConfiguration : BaseConfiguration<Buyer>
    {
        public override void Configure(EntityTypeBuilder<Buyer> buyerConfiguration)
        {
            buyerConfiguration.ToTable("buyers", OrderingContext.DEFAULT_SCHEMA);

            buyerConfiguration.Property(b => b.Id)
                .UseHiLo("buyerseq", OrderingContext.DEFAULT_SCHEMA);

            buyerConfiguration.Property(b => b.IdentityGuid)
                .HasMaxLength(200)
                .IsRequired();

            buyerConfiguration.HasIndex("IdentityGuid")
              .IsUnique(true);

            buyerConfiguration.Property(b => b.Name);

            buyerConfiguration.HasMany(b => b.PaymentMethods)
               .WithOne()
               .HasForeignKey("BuyerId")
               .OnDelete(DeleteBehavior.Cascade);

            var navigation = buyerConfiguration.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            base.Configure(buyerConfiguration);
        }
    }
}
