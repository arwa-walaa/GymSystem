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
    public class TrainerConfigration : GymUserConfigration<Entities.Trainer>, IEntityTypeConfiguration<Entities.Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            //JoinDate
            builder.Property(X => X.CreatedAt)
                .HasColumnName("HireDate")
                .HasDefaultValueSql("getdate()");



            base.Configure(builder);

        }
    }
}
