using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectYogaMed.Data;
using ProjectYogaMed.Models;

namespace ProjectYogaMed.Controllers
{
    [Authorize(Policy = "writepolicy")]
    public class DiseaseTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiseaseTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiseaseTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiseaseTable.ToListAsync());
        }

        // GET: DiseaseTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseTable = await _context.DiseaseTable
                .FirstOrDefaultAsync(m => m.DiseaseId == id);
            if (diseaseTable == null)
            {
                return NotFound();
            }

            return View(diseaseTable);
        }

        // GET: DiseaseTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiseaseTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiseaseId,DiseaseName")] DiseaseTable diseaseTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diseaseTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diseaseTable);
        }

        // GET: DiseaseTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseTable = await _context.DiseaseTable.FindAsync(id);
            if (diseaseTable == null)
            {
                return NotFound();
            }
            return View(diseaseTable);
        }

        // POST: DiseaseTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiseaseId,DiseaseName")] DiseaseTable diseaseTable)
        {
            if (id != diseaseTable.DiseaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diseaseTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseTableExists(diseaseTable.DiseaseId))
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
            return View(diseaseTable);
        }

        // GET: DiseaseTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseTable = await _context.DiseaseTable
                .FirstOrDefaultAsync(m => m.DiseaseId == id);
            if (diseaseTable == null)
            {
                return NotFound();
            }

            return View(diseaseTable);
        }

        // POST: DiseaseTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diseaseTable = await _context.DiseaseTable.FindAsync(id);
            _context.DiseaseTable.Remove(diseaseTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseaseTableExists(int id)
        {
            return _context.DiseaseTable.Any(e => e.DiseaseId == id);
        }
    }
}
