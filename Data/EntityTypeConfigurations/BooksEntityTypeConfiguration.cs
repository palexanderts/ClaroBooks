using Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityTypeConfigurations
{
    public sealed class BooksEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.PageCount).IsRequired();
            builder.Property(b => b.Excerpt).IsRequired();
            builder.Property(b => b.PublishDate).IsRequired();
        }
    }
}
