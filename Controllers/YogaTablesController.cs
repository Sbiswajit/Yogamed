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
using ProjectYogaMed.ViewModels;

namespace ProjectYogaMed.Controllers
{[Authorize(Policy = "writepolicy")]
    public class YogaTablesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public YogaTablesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: YogaTables
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.YogaTable.Include(y => y.Ydfk);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: YogaTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaTable = await _context.YogaTable
                .Include(y => y.Ydfk)
                .FirstOrDefaultAsync(m => m.YogaId == id);
            if (yogaTable == null)
            {
                return NotFound();
            }

            return View(yogaTable);
        }

        // GET: YogaTables/Create
        public IActionResult Create()
        {
            ViewData["YdfkId"] = new SelectList(_context.DiseaseTable, "DiseaseId", "DiseaseId");
            return View();
        }

        // POST: YogaTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewFile model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFilename = null;
                if (model.formFile != null)
                {
                    string ext = System.IO.Path.GetExtension(model.formFile.FileName);
                    if (ext == ".pdf")
                    {
                        string upload = System.IO.Path.Combine(_env.WebRootPath, "images");
                        uniqueFilename = Guid.NewGuid().ToString() + "_" + model.formFile.FileName;
                        string filepath = System.IO.Path.Combine(upload, uniqueFilename);
                        model.formFile.CopyTo(new System.IO.FileStream(filepath, System.IO.FileMode.Create));
                        //string filepath = $"{_env.WebRootPath}/images/{model.formFile.FileName}";
                        //var stream = System.IO.File.Create(filepath);
                        //model.formFile.CopyTo(stream);
                    }
                    else
                    {
                        TempData["Error"] = "Only Pdf files are allowed";
                        return RedirectToAction("Create", "Files");
                    }
                }


                YogaTable newfile = new YogaTable
                {
                    YogaId=model.YogaId,
                   YogaName = model.YogaName,
                    YogaStep=uniqueFilename,
                    Ydfk=model.Ydfk,
                    YdfkId=model.YdfkId,

                };
                _context.Add(newfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View();
        }

        // GET: YogaTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaTable = await _context.YogaTable.FindAsync(id);
            if (yogaTable == null)
            {
                return NotFound();
            }
            ViewData["YdfkId"] = new SelectList(_context.DiseaseTable, "DiseaseId", "DiseaseId", yogaTable.YdfkId);
            return View(yogaTable);
        }

        // POST: YogaTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YogaId,YogaName,YogaStep,YdfkId")] YogaTable yogaTable)
        {
            if (id != yogaTable.YogaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yogaTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YogaTableExists(yogaTable.YogaId))
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
            ViewData["YdfkId"] = new SelectList(_context.DiseaseTable, "DiseaseId", "DiseaseId", yogaTable.YdfkId);
            return View(yogaTable);
        }

        // GET: YogaTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaTable = await _context.YogaTable
                .Include(y => y.Ydfk)
                .FirstOrDefaultAsync(m => m.YogaId == id);
            if (yogaTable == null)
            {
                return NotFound();
            }

            return View(yogaTable);
        }

        // POST: YogaTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yogaTable = await _context.YogaTable.FindAsync(id);
            _context.YogaTable.Remove(yogaTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YogaTableExists(int id)
        {
            return _context.YogaTable.Any(e => e.YogaId == id);
        }
    }
}
