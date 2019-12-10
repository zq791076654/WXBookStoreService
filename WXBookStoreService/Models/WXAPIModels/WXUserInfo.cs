using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.WXAPIModels
{
    public class WXUserInfo
    {
        public string openId { get; set; }
        public string nickName { get; set; }
        public int gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public string unionId { get; set; }
        public WaterMark watermark { get; set; }
    }

    public class WaterMark
    {
        public string appid { get; set; }
        public int timestamp { get; set; }
    }
}