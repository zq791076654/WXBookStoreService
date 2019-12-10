using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WXBookStoreService.Models;
using WXBookStoreService.Models.DBModels;

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
    }
}
