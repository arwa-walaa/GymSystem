using GymSystemDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Configrations
{
    public class GymUserConfigration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(X=>X.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(X => X.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(X => X.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            builder.ToTable(TB =>
            {
                TB.HasCheckConstraint("GymUserValidEmailCheck","Email Like '_%@_%._&' ");
                TB.HasCheckConstraint("GymUserValidPhoneCheck", "Phone Like '01%' and Phone Not Like '%[^0-9]%' ");
            });
            //umique

            builder.HasIndex(X => X.Email).IsUnique();
            builder.HasIndex(X => X.Phone).IsUnique();

            builder.OwnsOne(X => X.Address, AddressBulider =>
            {
                AddressBulider.Property(a => a.Street)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .HasColumnName("Street");

                AddressBulider.Property(a => a.City)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .HasColumnName("City");
             
            });

        }
    }
}
