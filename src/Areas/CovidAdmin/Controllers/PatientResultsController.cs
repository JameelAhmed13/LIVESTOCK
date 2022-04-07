using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.CovidLab.Models;
using LIVESTOCK.Data;

namespace LIVESTOCK.Areas.CovidAdmin.Controllers
{
    [Area("CovidAdmin")]
    public class PatientResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CovidAdmin/PatientResults
        public ActionResult Index()
        {
            List<Lab> labList = new List<Lab>();
            labList = _context.Lab.ToList();
            labList.Insert(0, new Lab { LabID = 0, Name = "All" });
            ViewData["LabID"] = new SelectList(labList, "LabID", "Name");
            return View();
        }
        public ActionResult Dashboard(int labID)
        {
            if (labID == 0)
            {
                ViewBag.TotalTest = _context.Patient.Sum(a => a.TotalTestConducted);
                ViewBag.Pending = _context.Patient.Count(a => a.Status == 0);
                ViewBag.Nagitive = _context.Patient.Count(a => a.Status == 1);
                ViewBag.Positive = _context.Patient.Count(a => a.Status == 2);
            }
            else
            {
                ViewBag.TotalTest = _context.Patient.Sum(a => a.TotalTestConducted);
                ViewBag.Pending = _context.Patient.Count(a => a.Status == 0 && a.LabID == labID);
                ViewBag.Nagitive = _context.Patient.Count(a => a.Status == 1 && a.LabID == labID);
                ViewBag.Positive = _context.Patient.Count(a => a.Status == 2 && a.LabID == labID);
            }

            return PartialView();
        }
        // GET: CovidAdmin/PatientResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientResults = await _context.PatientResults
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PatientResultID == id);
            if (patientResults == null)
            {
                return NotFound();
            }

            return View(patientResults);
        }
       
    }
}
