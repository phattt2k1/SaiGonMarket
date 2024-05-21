using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SaiGonMarket.Models;
using System.Linq;

namespace SaiGonMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin.html", Name = "AdminIndex")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly dbMarketsContext _context;
        public HomeController(dbMarketsContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var Orders = _context.Orders.Include(o => o.Customer).Include(o => o.TransactStatus)
                .AsNoTracking()
                .OrderBy(x => x.OrderDate);
            PagedList<Order> models = new PagedList<Order>(Orders, pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;



            return View(models);
        }
    }
}
