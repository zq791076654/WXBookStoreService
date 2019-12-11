using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.DBModels.Mapping
{
    public class UserInfoMap : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoMap()
        {
            HasKey(p => p.Id).Property(p => p.Id);
            Property(p => p.Token);
            Property(p => p.OpenId);
            Property(p => p.SessionKey);
            Property(p => p.Unionid);
            Property(p => p.NickName);
            Property(p => p.Gender);
            Property(p => p.City);
            Property(p => p.Province);
            Property(p => p.Country);
            Property(p => p.AvatarUrl);
            Property(p => p.AppId);
            Property(p => p.TimeStamp);
            Property(p => p.Balance);

            HasMany(p => p.Orders).WithOptional(p => p.UserInfo).HasForeignKey(p => p.UserId);
            HasMany(p => p.Comments).WithOptional(p => p.UserInfo).HasForeignKey(p => p.UserId);

            ToTable("UserInfo");
        }
    }
}