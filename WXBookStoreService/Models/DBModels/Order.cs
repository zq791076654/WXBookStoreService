using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXBookStoreService.Models.DBModels
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderTime { get; set; }

        public UserInfo UserInfo { get; set; }
        public Book Book { get; set; }
    }
}