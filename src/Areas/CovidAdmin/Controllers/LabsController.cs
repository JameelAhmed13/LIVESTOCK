using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.CovidLab.Models;
using LIVESTOCK.Data;
using Microsoft.AspNetCore.Authorization;

namespace LIVESTOCK.Areas.CovidAdmin.Controllers
{
    [Area("CovidAdmin")]
    public class LabsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LabsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CovidAdmin/Labs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lab.Include(l => l.District);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: CovidAdmin/Labs
        public async Task<IActionResult> Index2()
        {
            var applicationDbContext = _context.Lab.Include(l => l.District);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        // GET: CovidAdmin/Labs/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Lab
                .Include(l => l.District)
                .FirstOrDefaultAsync(m => m.LabID == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        [Authorize(Roles = "CovidAdmin")]
        // GET: CovidAdmin/Labs/Create
        public IActionResult Create()
        {
            var districtList = _context.Districts
           .Select(x => new SelectListItem { Text = x.DistrictName, Value = x.DistrictID.ToString() })
           .ToList();
            districtList.Insert(0, new SelectListItem { Text = "Choose a District", Value = "" });
            ViewData["DistrictID"] = districtList;
            return View();
        }

        // POST: CovidAdmin/Labs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CovidAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LabID,Name,Address,ContactNo,MobileNo,DistrictID")] Lab lab)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["DistrictID"] = new SelectList(_context.Districts, "DistrictID", "DistrictName", lab.DistrictID);
            return View(lab);
        }

        [Authorize(Roles = "CovidAdmin")]
        // GET: CovidAdmin/Labs/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Lab.FindAsync(id);
            if (lab == null)
            {
                return NotFound();
            }
            ViewData["DistrictID"] = new SelectList(_context.Districts, "DistrictID", "DistrictName", lab.DistrictID);
            return View(lab);
        }

        // POST: CovidAdmin/Labs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CovidAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("LabID,Name,Address,ContactNo,MobileNo,DistrictID")] Lab lab)
        {
            if (id != lab.LabID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabExists(lab.LabID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Create));
            }
            ViewData["DistrictID"] = new SelectList(_context.Districts, "DistrictID", "DistrictName", lab.DistrictID);
            return View(lab);
        }

        // GET: CovidAdmin/Labs/Delete/5
        //public async Task<IActionResult> Delete(short? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var lab = await _context.Lab
        //        .Include(l => l.District)
        //        .FirstOrDefaultAsync(m => m.LabID == id);
        //    if (lab == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(lab);
        //}

        //// POST: CovidAdmin/Labs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(short id)
        //{
        //    var lab = await _context.Lab.FindAsync(id);
        //    _context.Lab.Remove(lab);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool LabExists(short id)
        {
            return _context.Lab.Any(e => e.LabID == id);
        }       
    }
}
