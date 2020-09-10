using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreIdentity.Model.Data.EntityTypeConfigurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address", "SalesLT");

            builder.HasIndex(e => e.Rowguid)
                .HasName("AK_Address_rowguid")
                .IsUnique();

            builder.HasIndex(e => e.StateProvince);

            builder.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvince, e.PostalCode, e.CountryRegion });

            builder.Property(e => e.AddressId).HasColumnName("AddressID");

            builder.Property(e => e.AddressLine1)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.AddressLine2).HasMaxLength(60);

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.CountryRegion)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.PostalCode)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.StateProvince)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
