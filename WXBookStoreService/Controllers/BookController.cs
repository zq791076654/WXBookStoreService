using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WXBookStoreService.Models;
using WXBookStoreService.Models.DBModels;
using WXBookStoreService.Models.ViewModels;

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
                CommentTime = t.CommentTime
            }));
        }

        [HttpGet]
        public IHttpActionResult AddComment(string token, int bookId,string comment)
        {
            UserInfo user = dbContext.UserInfos.FirstOrDefault(t => t.Token == token);
            Comment commentNew = new Comment()
            {
                UserId = user.Id,
                BookId = bookId,
                Content= comment,
                CommentTime = DateTime.Now
            };
            dbContext.Comments.Add(commentNew);
            dbContext.SaveChanges();

            return Json(true);
        }
    }
}
