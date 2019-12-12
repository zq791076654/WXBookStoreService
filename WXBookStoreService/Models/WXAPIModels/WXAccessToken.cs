using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.WXAPIModels
{
    public class WXAccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}