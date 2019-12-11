using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.DBModels.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            HasKey(p => p.Id).Property(p => p.Id);
            Property(p => p.Price);
            Property(p => p.OrderTime);

            ToTable("Order");
        }
    }
}