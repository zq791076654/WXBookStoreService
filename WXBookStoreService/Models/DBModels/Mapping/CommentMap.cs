using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.DBModels.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            HasKey(p => p.Id).Property(p => p.Id);
            Property(p => p.Content);
            Property(p => p.CommentTime);

            ToTable("Comment");
        }
    }
}