using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.website.Models;
using LIVESTOCK.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class MainSlidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainSlidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: website/MainSliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.MainSlider.OrderBy(a=>a.OrderSequence).ToListAsync());
        }
        // GET: website/MainSliders
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.MainSlider.OrderBy(a=>a.OrderSequence).ToListAsync());
        }      
        [Authorize(Roles = "Website")]
        // GET: website/MainSliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: website/MainSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainSliderID,Title,PicturePath,OrderSequence,CreatedOn,Visibility")] MainSlider mainSlider, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                if (Attachment != null)
                {
                    var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\MainSlider\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    //fileName = fileName.Replace(".", "-");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "MainSlider" + randomNumber.ToString() + fileName;
                    mainSlider.PicturePath = Path.Combine("/Documents/MainSlider/", fileName);//Server Path
                    string sPath = Path.Combine(rootPath);
                    if (!System.IO.Directory.Exists(sPath))
                    {
                        System.IO.Directory.CreateDirectory(sPath);
                    }
                    string FullPathWithFileName = Path.Combine(sPath, fileName);
                    using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                    {
                        await Attachment.CopyToAsync(stream);
                    }

                    Image_resize(rootPath + fileName, rootPath + fileName.Replace(".", "1."), 1800, 600);
                    mainSlider.PicturePath = mainSlider.PicturePath.Replace(".", "1.");
                    mainSlider.Visibility = true;
                    mainSlider.CreatedOn = DateTime.Now.Date;
                    _context.Add(mainSlider);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Create));
            }
            return View(mainSlider);
        }
        [Authorize(Roles = "Website")]
        // GET: website/MainSliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainSlider = await _context.MainSlider.FindAsync(id);
            if (mainSlider == null)
            {
                return NotFound();
            }
            return View(mainSlider);
        }

        // POST: website/MainSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MainSliderID,Title,PicturePath,OrderSequence,CreatedOn,Visibility")] MainSlider mainSlider)
        {
            if (id != mainSlider.MainSliderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainSlider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainSliderExists(mainSlider.MainSliderID))
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
            return View(mainSlider);
        }
        [Authorize(Roles = "Website")]
        // GET: website/MainSliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainSlider = await _context.MainSlider
                .FirstOrDefaultAsync(m => m.MainSliderID == id);
            if (mainSlider == null)
            {
                return NotFound();
            }

            return View(mainSlider);
        }

        // POST: website/MainSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainSlider = await _context.MainSlider.FindAsync(id);
            _context.MainSlider.Remove(mainSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Delete3")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed3(int id, string save, string remove, int neworder)
        {
            var mainSlider = await _context.MainSlider.FindAsync(id);
            if(save != null && neworder != 0)
            {
                mainSlider.OrderSequence = neworder;
                _context.Update(mainSlider);
            }
            if(remove != null)
            {
                _context.MainSlider.Remove(mainSlider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }
        private bool MainSliderExists(int id)
        {
            return _context.MainSlider.Any(e => e.MainSliderID == id);
        }

        private void Image_resize(string input_Image_Path, string output_Image_Path, int width, int height)
        {
            const long quality = 50L;
            using (var image = new Bitmap(System.Drawing.Image.FromFile(input_Image_Path)))
            {
                if (image.Width > 1200)
                {
                    width = image.Width;
                }                
                var resized_Bitmap = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resized_Bitmap))
                {                    
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, width, height);
                    using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))
                    {
                        var qualityParamId = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                        var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                        resized_Bitmap.Save(output, codec, encoderParameters);
                    }
                }
            }
        }
    }
}
