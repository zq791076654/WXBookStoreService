using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WXBookStoreService.Models;
using WXBookStoreService.Models.DBModels;
using WXBookStoreService.Models.ViewModels;
using WXBookStoreService.Models.WXAPIModels;

namespace WXBookStoreService.Controllers
{
    public class BookController : ApiController
    {
        BSContext dbContext = new BSContext();

        [HttpGet]
        public IHttpActionResult GetBookById(int id)
        {
            Book book = dbContext.Books.FirstOrDefault(t => t.Id == id);
            return Json(book);
        }

        [HttpGet]
        public IHttpActionResult GetBookList()
        {
            List<Book> books = dbContext.Books.ToList();
            return Json(books);
        }

        [HttpGet]
        public IHttpActionResult BuyBook(string token, int bookId)
        {
            UserInfo user = dbContext.UserInfos.FirstOrDefault(t => t.Token == token);
            Book book = dbContext.Books.FirstOrDefault(t => t.Id == bookId);

            Order order = new Order();
            order.UserId = user.Id;
            order.BookId = book.Id;
            order.Price = book.Price;
            order.OrderTime = DateTime.Now;
            dbContext.Orders.Add(order);

            user.Balance -= order.Price;
            dbContext.SaveChanges();

            return Json(VMUserInfo.GetVMUserInfo(user));
        }

        [HttpGet]
        public IHttpActionResult Prepay(string token)
        {
            UserInfo user = dbContext.UserInfos.FirstOrDefault(t => t.Token == token);
            user.Balance += 300m;
            dbContext.SaveChanges();
            return Json(VMUserInfo.GetVMUserInfo(user));
        }

        [HttpGet]
        public IHttpActionResult IsBuy(string token, int bookId)
        {
            bool isBuy = dbContext.Orders.Any(t => t.BookId == bookId && t.UserInfo.Token == token);
            return Json(isBuy);
        }

        [HttpGet]
        public IHttpActionResult BookCount(string token)
        {
            int count = dbContext.Orders.Count(t => t.UserInfo.Token == token);
            return Json(count);
        }

        [HttpGet]
        public IHttpActionResult GetComments(int bookId)
        {
            List<Comment> comment = dbContext.Comments.Include("UserInfo").Where(t => t.BookId == bookId).OrderByDescending(t => t.CommentTime).ToList();

            return Json(comment.Select(t => new VMComment()
            {
                UserToken = t.UserInfo.Token,
                UserAvatarUrl = t.UserInfo.AvatarUrl,
                UserNickName = t.UserInfo.NickName,
                Content = t.Content,
                CommentTime = t.CommentTime.ToString("yyyy-MM-dd HH:mm:ss")
            }));
        }

        [HttpGet]
        public IHttpActionResult AddComment(string token, int bookId, string comment)
        {
            UserInfo user = dbContext.UserInfos.FirstOrDefault(t => t.Token == token);
            Comment commentNew = new Comment()
            {
                UserId = user.Id,
                BookId = bookId,
                Content = comment,
                CommentTime = DateTime.Now
            };
            dbContext.Comments.Add(commentNew);
            dbContext.SaveChanges();

            //WXAccessToken wxAccessToken = JsonConvert.DeserializeObject<WXAccessToken>( HttpHelper.HttpGet("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + HttpHelper.AppId + "&secret=" + HttpHelper.AppSecret));

            //HttpHelper.HttpPost("https://api.weixin.qq.com/cgi-bin/message/subscribe/send?access_token="+ wxAccessToken.access_token,
            //    JsonConvert.SerializeObject(
            //        new {
            //            touser = user.OpenId,
            //            template_id = "ilyGe99yk5k0tzD6FVMcpo4bwJP8KKPAowV13qE0ikg",
            //            page = "/pages/index/index",
            //            data = new
            //            {
            //                amount2=new { value = "￥100"},
            //                time4 = new { value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
            //                amount3 = new { value = "￥123" },
            //            }
            //        })
            //    );

            return Json(true);
        }

        [HttpGet]
        public HttpResponseMessage GetBook(int bookId)
        {
            Book book = dbContext.Books.FirstOrDefault(t => t.Id == bookId);

            string fileName = book.Name+".pdf";
            string filePath = HttpContext.Current.Server.MapPath("~/") + "files\\" + book.Name + ".pdf";
            FileStream stream = new FileStream(filePath, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = HttpUtility.UrlEncode(fileName)
            };
            response.Headers.Add("Access-Control-Expose-Headers", "FileName");
            response.Headers.Add("FileName", HttpUtility.UrlEncode(fileName));
            return response;
        }

        [HttpGet]
        public IHttpActionResult GetMyBookList(string token)
        {
            List<Book> books = dbContext.Books.Where(t=>t.Orders.Any(o=>o.UserInfo.Token == token)).ToList();
            return Json(books);
        }
    }
}
