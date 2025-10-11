using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Configrations
{
    public class MamberSessionConfigration : IEntityTypeConfiguration<Entities.MemberSession>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.MemberSession> builder)
        {
            builder.Ignore(ms => ms.Id);


            builder.HasKey(ms => new { ms.MemberId, ms.SessionId });
            builder.Property(ms=>ms.CreatedAt)
                      .HasColumnName("BookingDate")
                     .HasDefaultValueSql("getdate()");


       
        }
    }
  
}
