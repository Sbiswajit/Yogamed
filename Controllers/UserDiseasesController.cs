using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectYogaMed.Data;
using ProjectYogaMed.Models;

namespace ProjectYogaMed.Controllers
{ 
[Authorize(Policy = "writepolicyuser")]
public class UserDiseasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public UserDiseasesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: UserDiseases
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserDisease.ToListAsync());
        }

        // GET: UserDiseases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDisease = await _context.UserDisease
                .FirstOrDefaultAsync(m => m.Udid == id);
            if (userDisease == null)
            {
                return NotFound();
            }

            return View(userDisease);
        }
        public async Task<IActionResult> Display()
        {
            var applicationDbContext = _context.YogaTable.Include(y => y.Ydfk);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserDiseases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserDiseases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Udid,UserIdFk,Disease,DiseaseIdfk")] UserDisease userDisease)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDisease);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userDisease);
        }
        public async Task<IActionResult> Download(int? id)
        {
            var filename = await _context.YogaTable.FindAsync(id);
            var data = filename.YogaStep;
            string upload = System.IO.Path.Combine(_env.WebRootPath, "images");
            string filepath = System.IO.Path.Combine(upload, data);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            string file = data;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file);

        }
        // GET: UserDiseases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDisease = await _context.UserDisease.FindAsync(id);
            if (userDisease == null)
            {
                return NotFound();
            }
            return View(userDisease);
        }

        // POST: UserDiseases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Udid,UserIdFk,Disease,DiseaseIdfk")] UserDisease userDisease)
        {
            if (id != userDisease.Udid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDisease);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDiseaseExists(userDisease.Udid))
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
            return View(userDisease);
        }

        // GET: UserDiseases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDisease = await _context.UserDisease
                .FirstOrDefaultAsync(m => m.Udid == id);
            if (userDisease == null)
            {
                return NotFound();
            }

            return View(userDisease);
        }

        // POST: UserDiseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userDisease = await _context.UserDisease.FindAsync(id);
            _context.UserDisease.Remove(userDisease);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDiseaseExists(int id)
        {
            return _context.UserDisease.Any(e => e.Udid == id);
        }
    }
}
