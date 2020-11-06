using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NTest.Data;
using NTest.Models;

namespace NTest.Controllers
{
    public class LidaDatasController : Controller
    {
        private MVCLidaContext _context;
        private IWebHostEnvironment _hostingEnvironment;

        public LidaDatasController(MVCLidaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        // GET: LidaDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.LidaData.ToListAsync());
        }

        // GET: LidaDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lidaData = await _context.LidaData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lidaData == null)
            {
                return NotFound();
            }

            return View(lidaData);
        }

        // GET: LidaDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LidaDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SlopeAngle,ImageClock,LidarClock,PrependDist,GroudDist")] LidaData lidaData, IFormFile imageFile)
        {
            

            if (ModelState.IsValid)
            {
                
                string fileId = Guid.NewGuid().ToString().Replace("-", "");
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var finalFilename =fileId + "-" + Path.GetFileName(imageFile.FileName);

                var path = Path.Combine(uploads, finalFilename);

                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }



                lidaData.ImageURL = finalFilename;

                _context.Add(lidaData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lidaData);
        }

        // GET: LidaDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lidaData = await _context.LidaData.FindAsync(id);
            if (lidaData == null)
            {
                return NotFound();
            }
            return View(lidaData);
        }

        // POST: LidaDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageURL,SlopeAngle,ImageClock,LidarClock,PrependDist,GroudDist")] LidaData lidaData)
        {
            if (id != lidaData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lidaData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LidaDataExists(lidaData.Id))
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
            return View(lidaData);
        }

        // GET: LidaDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lidaData = await _context.LidaData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lidaData == null)
            {
                return NotFound();
            }

            return View(lidaData);
        }

        // POST: LidaDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lidaData = await _context.LidaData.FindAsync(id);
            _context.LidaData.Remove(lidaData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LidaDataExists(int id)
        {
            return _context.LidaData.Any(e => e.Id == id);
        }
    }
}
