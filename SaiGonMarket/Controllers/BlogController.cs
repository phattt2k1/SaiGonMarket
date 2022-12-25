using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SaiGonMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaiGonMarket.Controllers
{
    public class BlogController : Controller
    {
        private readonly dbMarketsContext _context;
        public BlogController(dbMarketsContext context)
        {
            _context = context;
        }
        // GET: Blogs/Index
        [Route("blogs.html", Name = "Blog")]
        public IActionResult Index(int? page)
        {
            //Phan trang
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsTinDangs = _context.TblTinTucs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            PagedList<TblTinTuc> models = new PagedList<TblTinTuc>(lsTinDangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Blogs/Details/5
        [Route("/tin-tuc/{Alias}-{id}.html", Name = "TinDetails")]
        public IActionResult Details(int id)
        {
            var tindang = _context.TblTinTucs.AsNoTracking().SingleOrDefault(x => x.PostId == id);
            if (tindang == null)
            {
                return RedirectToAction("Index");
            }
            var lsRelated = _context.TblTinTucs
                .AsNoTracking()
                .Where(x => x.Published == true && x.PostId != id)
                .Take(3)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
            ViewBag.Related = lsRelated;
            return View(tindang);
        }
    }
}
