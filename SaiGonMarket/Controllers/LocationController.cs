using Microsoft.AspNetCore.Mvc;
using SaiGonMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaiGonMarket.Controllers
{
    public class LocationController : Controller
    {
        private readonly dbMarketsContext _context;

        public LocationController(dbMarketsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region ============GET Location ========================
        public ActionResult QuanHuyenList(int LocationId)
        {
            var QuanHuyens = _context.Locations.OrderBy(x => x.LocationId)
                .Where(x => x.ParentCode == LocationId && x.Levels == 2)
                .OrderBy(x => x.Name)
                .ToList();
            return Json(QuanHuyens);
        }

        public ActionResult PhuongXaList(int LocationId)
        {
            var PhuongXas = _context.Locations.OrderBy(x => x.LocationId)
                .Where(x => x.ParentCode == LocationId && x.Levels == 3)
                .OrderBy(x => x.Name)
                .ToList();
            return Json(PhuongXas);
        }
        #endregion================================================
    }
}
