using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.DBModels.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            HasKey(p => p.Id).Property(p => p.Id);
            Property(p => p.Name);
            Property(p => p.Author);
            Property(p => p.Price);
            Property(p => p.CoverUrl);

            HasMany(p => p.Orders).WithOptional(p => p.Book).HasForeignKey(p => p.BookId);
            HasMany(p => p.Comments).WithOptional(p => p.Book).HasForeignKey(p => p.BookId);
            ToTable("Book");
        }

    }
}