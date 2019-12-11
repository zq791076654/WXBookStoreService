using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.DBModels
{
    public enum gender
    {
        未知,
        男,
        女
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string OpenId { get; set; }
        public string SessionKey { get; set; }
        public string Unionid { get; set; }
        public string NickName { get; set; }
        public gender Gender { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string AvatarUrl { get; set; }
        public string AppId { get; set; }
        public int TimeStamp { get; set; }
        public decimal Balance { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}