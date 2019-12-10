using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WXBookStoreService.Models.DBModels;

namespace WXBookStoreService.Models.ViewModels
{
    public class VMUserInfo
    {
        public string Token { get; set; }
        public string NickName { get; set; }
        public int Gender { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string AvatarUrl { get; set; }

        public static VMUserInfo GetVMUserInfo(UserInfo userInfo)
        {
            return new VMUserInfo()
            {
                Token = userInfo.Token,
                NickName = userInfo.NickName,
                Gender = (int)userInfo.Gender,
                City = userInfo.City,
                Province = userInfo.Province,
                Country = userInfo.Country,
                AvatarUrl = userInfo.AvatarUrl,
            };
        }
    }
}