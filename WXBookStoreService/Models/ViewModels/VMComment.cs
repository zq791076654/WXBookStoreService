using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.ViewModels
{
    public class VMComment
    {
        public string UserToken { get; set; }
        public string UserAvatarUrl { get; set; }
        public string UserNickName { get; set; }
        public string Content { get; set; }
        public string CommentTime { get; set; }

    }
}