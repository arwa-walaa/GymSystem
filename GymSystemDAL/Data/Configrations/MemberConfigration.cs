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
    public class MemberConfigration : GymUserConfigration<Entities.Member>, IEntityTypeConfiguration<Entities.Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            //JoinDate
            builder.Property(X=> X.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("getdate()");

        

            base.Configure(builder);

        }
    }
}
