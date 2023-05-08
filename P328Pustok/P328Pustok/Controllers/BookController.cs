using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P328Pustok.DAL;
using P328Pustok.Models;
using P328Pustok.ViewModels;

namespace P328Pustok.Controllers
{
    public class BookController : Controller
    {
        private readonly PustokContext _context;

        public BookController(PustokContext context)
        {
            _context = context;
        }
        public IActionResult GetBookDetail(int id)
        {
            Book book = _context.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Include(x=>x.BookTags).ThenInclude(x=>x.Tag)
                .FirstOrDefault(x => x.Id == id);

            if (book == null) return StatusCode(404);

            //return Json(new { book = book });
            return PartialView("_BookModalPartial", book);
        }

        public IActionResult AddToBasket(int id)
        {
            List<BasketItemCookieViewModel> cookieItems = new List<BasketItemCookieViewModel>();

            BasketItemCookieViewModel cookieItem;
            var basketStr = Request.Cookies["basket"];
            if (basketStr != null)
            {
                cookieItems = JsonConvert.DeserializeObject<List<BasketItemCookieViewModel>>(basketStr);

                cookieItem = cookieItems.FirstOrDefault(x => x.BookId == id);

                if (cookieItem != null)
                    cookieItem.Count++;
                else
                {
                    cookieItem = new BasketItemCookieViewModel { BookId = id, Count = 1 };
                    cookieItems.Add(cookieItem);
                }
            }
            else
            {
                cookieItem = new BasketItemCookieViewModel { BookId = id, Count = 1 };
                cookieItems.Add(cookieItem);
            }

            Response.Cookies.Append("Basket", JsonConvert.SerializeObject(cookieItems));
            return RedirectToAction("index", "home");
        }

        public IActionResult ShowBasket()
        {
            var basket = new List<BasketItemCookieViewModel>();
            var basketStr = Request.Cookies["basket"];

            if(basketStr !=null )
                 basket = JsonConvert.DeserializeObject<List<BasketItemCookieViewModel>>(basketStr);

            return Json(new { basket });
        }
    }
}
