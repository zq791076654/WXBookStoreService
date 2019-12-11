using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.DBModels
{
    public class Comment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public string Content { get; set; }
        public DateTime CommentTime { get; set; }

        public UserInfo UserInfo { get; set; }
        public Book Book { get; set; }
    }
}