using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Data;
using LIVESTOCK.Areas.CovidLab.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LIVESTOCK.Areas.CovidLab.Controllers
{
    [Area("CovidLab")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CovidLab/Patients
        public async Task<IActionResult> Index()
        {
            short LabId = await GetCurrentLabId();
            var applicationDbContext = _context.Patient.Include(p => p.District).Where(a=>a.LabID == LabId);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: CovidLab/Patients
        public async Task<IActionResult> Index2()
        {
            short LabId = await GetCurrentLabId();
            var applicationDbContext = _context.Patient.Include(p => p.District).Where(a=>a.Status == 0 && a.LabID == LabId);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        // GET: CovidLab/Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.District)
                .FirstOrDefaultAsync(m => m.PatientID == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: CovidLab/Patients/Create
        [Authorize(Roles = "CovidLab")]
        public async Task<IActionResult> Create()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Male", Value = "Male" },
                new SelectListItem{ Text="Female", Value = "Female"},
            };
            ViewData["ddList"] = list;
            var districtList = _context.Districts
            .Select(x => new SelectListItem { Text = x.DistrictName, Value = x.DistrictID.ToString() })
            .ToList();
            districtList.Insert(0, new SelectListItem { Text = "Choose a District", Value = "" });
            ViewData["DistrictID"] = districtList;
            ViewBag.CurrentDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            Patient patient = new Patient();
            patient.LabID = await GetCurrentLabId();
            patient.TotalTestConducted = 0;
            return View(patient);
        }

        // POST: CovidLab/Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CovidLab")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientID,LabID,Code,Name,FatherHusbandName,MobileNo,Age,CNIC,Profession,DistrictID,Address,DOS,Gender,TotalTestConducted,Status,CreatedDate,CreatedBy")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.CreatedDate = DateTime.Now.Date;
                patient.Status = 0;
                patient.CreatedBy = User.Identity.Name;
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["DistrictID"] = new SelectList(_context.Districts, "DistrictID", "DistrictName", patient.DistrictID);
            return View(patient);
        }

        // GET: CovidLab/Patients/Edit/5
        [Authorize(Roles = "CovidLab")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Male", Value = "Male" },
                new SelectListItem{ Text="Female", Value = "Female"},
            };
            ViewData["ddList"] = list;
            ViewData["DistrictID"] = new SelectList(_context.Districts, "DistrictID", "DistrictName", patient.DistrictID);
            return View(patient);
        }

        // POST: CovidLab/Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CovidLab")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientID,LabID,Code,Name,FatherHusbandName,MobileNo,Age,CNIC,Profession,DistrictID,Address,DOS,Gender,TotalTestConducted,Status,CreatedDate,CreatedBy")] Patient patient)
        {
            if (id != patient.PatientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    patient.CreatedBy = User.Identity.Name;
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientID))
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
            ViewData["DistrictID"] = new SelectList(_context.Districts, "DistrictID", "DistrictName", patient.DistrictID);
            return View(patient);
        }

        // GET: CovidLab/Patients/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var patient = await _context.Patient
        //        .Include(p => p.District)
        //        .FirstOrDefaultAsync(m => m.PatientID == id);
        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(patient);
        //}

        //// POST: CovidLab/Patients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var patient = await _context.Patient.FindAsync(id);
        //    _context.Patient.Remove(patient);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Create));
        //}

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.PatientID == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task<short> GetCurrentLabId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();            
            return (usr.LabAccess);            
        }
    }
}
