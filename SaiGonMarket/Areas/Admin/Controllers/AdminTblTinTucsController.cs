using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SaiGonMarket.Helpper;
using SaiGonMarket.Models;

namespace SaiGonMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminTblTinTucsController : Controller
    {
        private readonly dbMarketsContext _context;
        public INotyfService _notyfService { get; }
        public AdminTblTinTucsController(dbMarketsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminTblTinTucs
        public IActionResult Index(int? page)
        {
            var collection = _context.TblTinTucs.AsNoTracking().ToList();
            foreach (var item in collection)
            {
                if (item.CreatedDate == null)
                {
                    item.CreatedDate = DateTime.Now;
                    _context.Update(item);
                    _context.SaveChanges();
                }
            }

            //Phan trang
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsPosts = _context.TblTinTucs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);

            PagedList<TblTinTuc> models = new PagedList<TblTinTuc>(lsPosts, pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminTblTinTucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTinTuc = await _context.TblTinTucs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tblTinTuc == null)
            {
                return NotFound();
            }

            return View(tblTinTuc);
        }

        // GET: Admin/AdminTblTinTucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTblTinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] TblTinTuc tblTinTuc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                //Xu ly thumb
                if (fThumb != null)
                {
                    //Lay ra extension
                    string extension = Path.GetExtension(fThumb.FileName);
                    //Chuan hoa sau do them extension
                    string imageName = Utilities.SEOUrl(tblTinTuc.Title) + extension;
                    //Upload hinh anh
                    tblTinTuc.Thumb = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
                }

                if (string.IsNullOrEmpty(tblTinTuc.Thumb)) tblTinTuc.Thumb = "default.jpg";
                tblTinTuc.Alias = Utilities.SEOUrl(tblTinTuc.Title);
                tblTinTuc.CreatedDate = DateTime.Now;   
                _context.Add(tblTinTuc);
                await _context.SaveChangesAsync();
                _notyfService.Success("Created successfully");
                return RedirectToAction(nameof(Index));
            }
            return View(tblTinTuc);
        }

        // GET: Admin/AdminTblTinTucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTinTuc = await _context.TblTinTucs.FindAsync(id);
            if (tblTinTuc == null)
            {
                return NotFound();
            }
            return View(tblTinTuc);
        }

        // POST: Admin/AdminTblTinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] TblTinTuc tblTinTuc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != tblTinTuc.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Xu ly thumb
                    if (fThumb != null)
                    {
                        //Lay ra extension
                        string extension = Path.GetExtension(fThumb.FileName);
                        //Chuan hoa sau do them extension
                        string imageName = Utilities.SEOUrl(tblTinTuc.Title) + extension;
                        //Upload hinh anh
                        tblTinTuc.Thumb = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
                    }

                    if (string.IsNullOrEmpty(tblTinTuc.Thumb)) tblTinTuc.Thumb = "default.jpg";
                    tblTinTuc.Alias = Utilities.SEOUrl(tblTinTuc.Title);

                    _context.Update(tblTinTuc);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Updated successfully");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTinTucExists(tblTinTuc.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblTinTuc);
        }

        // GET: Admin/AdminTblTinTucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTinTuc = await _context.TblTinTucs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tblTinTuc == null)
            {
                return NotFound();
            }

            return View(tblTinTuc);
        }

        // POST: Admin/AdminTblTinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblTinTuc = await _context.TblTinTucs.FindAsync(id);
            _context.TblTinTucs.Remove(tblTinTuc);
            await _context.SaveChangesAsync();
            _notyfService.Success("Deleted successfully");
            return RedirectToAction(nameof(Index));
        }

        private bool TblTinTucExists(int id)
        {
            return _context.TblTinTucs.Any(e => e.PostId == id);
        }
    }
}
