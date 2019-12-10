﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WXBookStoreService.Models.DBModels.Mapping;

namespace WXBookStoreService.Models.DBModels
{
    public class BSContext : DbContext
    {
        public BSContext() : base("name=WXBS")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new UserInfoMap());
        }
    }
}