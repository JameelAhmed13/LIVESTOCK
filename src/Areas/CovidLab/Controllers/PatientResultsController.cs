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
using LIVESTOCK.API;
using System.Net;
using System.Net.Http;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace LIVESTOCK.Areas.CovidLab.Controllers
{
    [Area("CovidLab")]
    public class PatientResultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientResultsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Dashboard()
        {
            short LabId = await GetCurrentLabId();
            ViewBag.TotalTest = _context.Patient.Count(a=>a.LabID == LabId);
            ViewBag.Pending = _context.Patient.Count(a => a.Status == 0 && a.LabID == LabId);
            ViewBag.Nagitive = _context.Patient.Count(a => a.Status == 1 && a.LabID == LabId);
            ViewBag.Positive = _context.Patient.Count(a => a.Status == 2 && a.LabID == LabId);
            return View();
        }
        // GET: CovidLab/PatientResults
        public async Task<IActionResult> Index()
        {
            short LabId = await GetCurrentLabId();
            var applicationDbContext = _context.PatientResults.Include(p => p.Patient).Where(a=>a.Patient.LabID == LabId);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: CovidLab/PatientResults
        public IActionResult SearchIndex()
        {
            return View();
        }
        public async Task<IActionResult> SearchedPatient(string text, string searchTo)
        {
            short LabId = await GetCurrentLabId();
            var list = _context.Patient.Where(a=>a.PatientID == 0 && a.LabID == LabId);
            switch (searchTo)
            {
                case "Name":
                    list = _context.Patient.Include(a => a.District).Where(a => a.Name.Contains(text) && a.LabID == LabId).Select(a => new Patient { Code = a.Code, Name = a.Name, CNIC = a.CNIC, FatherHusbandName = a.FatherHusbandName, MobileNo = a.MobileNo, Age = a.Age, Status = a.Status, Profession = a.District.DistrictName, PatientID = a.PatientID });//.Take(10);
                    break;
                case "CNIC":
                    list = _context.Patient.Include(a => a.District).Where(a => a.CNIC.Contains(text) && a.LabID == LabId).Select(a => new Patient { Code = a.Code, Name = a.Name, CNIC = a.CNIC, FatherHusbandName = a.FatherHusbandName, MobileNo = a.MobileNo, Age = a.Age, Status = a.Status, Profession = a.District.DistrictName, PatientID = a.PatientID });//.Take(10);
                    break;
                case "Code":
                    list = _context.Patient.Include(a => a.District).Where(a => a.Code.Contains(text) && a.LabID == LabId).Select(a => new Patient { Code = a.Code, Name = a.Name, CNIC = a.CNIC, FatherHusbandName = a.FatherHusbandName, MobileNo = a.MobileNo, Age = a.Age, Status = a.Status, Profession = a.District.DistrictName, PatientID = a.PatientID });//.Take(10);
                    break;
                default:
                    list = _context.Patient.Include(a => a.District).Where(a => a.FatherHusbandName.Contains(text) && a.LabID == LabId).Select(a => new Patient { Code = a.Code, Name = a.Name, CNIC = a.CNIC, FatherHusbandName = a.FatherHusbandName, MobileNo = a.MobileNo, Age = a.Age, Status = a.Status, Profession = a.District.DistrictName, PatientID = a.PatientID });//.Take(10);
                    break;
            }
            return PartialView(await list.ToListAsync());
        }
        public String Translate(String word)
        {
            var toLanguage = "ur";//English
            var fromLanguage = "en";//Deutsch
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTestResult(int PatientID, DateTime DOR, string ReTestDate, bool Status)
        {
            if (ModelState.IsValid)
            {
                PatientResults patientResult = new PatientResults();
                patientResult.PatientID = PatientID;
                patientResult.Result = Status;
                patientResult.DOR = DOR;
                patientResult.ReTestDate = DOR;
                patientResult.CreatedBy = User.Identity.Name;
                patientResult.CreatedDate = DateTime.Now.Date;
                _context.Add(patientResult);                
                Patient Obj = _context.Patient.Find(PatientID);
                Obj.Status = (short) (Status ? 2 : 1);
                Obj.TotalTestConducted++;
                await _context.SaveChangesAsync();              
                var patientObj = _context.Patient.Find(PatientID);
                //string msg = "Health Department!\nCovid-19 Result of Name: " + Obj.Name + ")\nCNIC: " + Obj.CNIC + "\nAge: " + Obj.Age + "\nResult: " + (patientResult.Result == true ? "Positive" : "Negitive") + "\nmore detail: 0819203996";
                string msg = "";
                if (patientResult.Result)
                {
                    msg = "محکمہ صحت!\nکوویڈ ۔19 کا نتیجہ\nنام: " + Translate(Obj.Name) + "\nشناختی کارڈ نمبر: " + Obj.CNIC.Replace("-", "") + "\nعمر: " + Obj.Age + "\nنتیجہ: " + (patientResult.Result == true ? "مثبت" : "منفی") + "\nدوبارہ ٹیسٹ کی تاریخ: "+ patientResult.ReTestDate.ToString("yyyy-MMM-dd") +"\nمزید معلومات کے لئے رابطہ کریں: 0819203996";
                    //msg = "Health Department!\nCovid-19 Result of Name: " + Obj.Name + "\nCNIC: " + Obj.CNIC + "\nAge: " + Obj.Age + "\nResult: " + (patientResult.Result == true ? "Positive" : "Negitive") + "\nmore detail: 0819203996";
                }
                else
                {
                    msg = "محکمہ صحت!\nکوویڈ ۔19 کا نتیجہ\nنام: " + Translate(Obj.Name) + "\nشناختی کارڈ نمبر: " + Obj.CNIC.Replace("-", "") + "\nعمر: " + Obj.Age + "\nنتیجہ: " + (patientResult.Result == true ? "مثبت" : "منفی") + "\nمزید معلومات کے لئے رابطہ کریں: 0819203996";
                    //msg = "Health Department!\nCovid-19 Result of Name: " + Obj.Name + "\nCNIC: " + Obj.CNIC + "\nAge: " + Obj.Age + "\nResult: " + (patientResult.Result == true ? "Positive" : "Negitive") + "\nmore detail: 0819203996";
                }
                
                //string msg = "وزہ کی حالت میں مسواک کے جواز میں تردد کرتے ہیں۔";
                ZongSMS ObjSMS = new ZongSMS();                               
                ObjSMS.SendSingleSMSCovid(msg, patientObj.MobileNo);                
                //---------------------------------------------------
                return Json(new { success = true, responseText = "1" });
            }
            return Json(new { success = false, responseText = "0" });
        }
        static String Reverse2(string str)
        {
            int strLen = str.Length, elem = strLen - 1;
            char[] charA = new char[strLen];

            for (int i = 0; i < strLen; i++)
            {
                charA[elem] = str[i];
                elem--;
            }

            return new String(charA);
        }
        [Authorize(Roles = "CovidLab")]
        public async Task<IActionResult> ProcessResult()
        {
            short LabId = await GetCurrentLabId();
            var patientList = _context.Patient.Include(p => p.District).Where(a=>(a.Status == 0 || a.Status == 2) && a.LabID == LabId).Take(20);
            ViewBag.CurrentDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            ViewBag.RemainingResult = (_context.Patient.Where(a => (a.Status == 0 || a.Status == 2) && a.LabID == LabId).Count() - patientList.Count());
            return View(await patientList.ToListAsync());
        }
        // GET: CovidLab/PatientResults/Details/5
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

        // GET: CovidLab/PatientResults/Create
        [Authorize(Roles = "CovidLab")]
        public IActionResult Create()
        {
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID");
            return View();
        }

        // POST: CovidLab/PatientResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CovidLab")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientResultID,PatientID,DOR,Result,ReTestDate,CreatedDate")] PatientResults patientResults)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientResults);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID", patientResults.PatientID);
            return View(patientResults);
        }

        [Authorize(Roles = "CovidLab")]
        // GET: CovidLab/PatientResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientResults = await _context.PatientResults.FindAsync(id);
            if (patientResults == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID", patientResults.PatientID);
            return View(patientResults);
        }

        // POST: CovidLab/PatientResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CovidLab")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientResultID,PatientID,DOR,Result,ReTestDate,CreatedDate")] PatientResults patientResults)
        {
            if (id != patientResults.PatientResultID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientResults);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientResultsExists(patientResults.PatientResultID))
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
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID", patientResults.PatientID);
            return View(patientResults);
        }

        // GET: CovidLab/PatientResults/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var patientResults = await _context.PatientResults
        //        .Include(p => p.Patient)
        //        .FirstOrDefaultAsync(m => m.PatientResultID == id);
        //    if (patientResults == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(patientResults);
        //}

        //// POST: CovidLab/PatientResults/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var patientResults = await _context.PatientResults.FindAsync(id);
        //    _context.PatientResults.Remove(patientResults);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PatientResultsExists(int id)
        {
            return _context.PatientResults.Any(e => e.PatientResultID == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task<short> GetCurrentLabId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return (usr.LabAccess);
        }
    }
}
