using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionStages.Data;
using GestionStages.Models.MilieuStage;

namespace GestionStages.Controllers
{
    public class EntrepriseTypeMilieuStagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntrepriseTypeMilieuStagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EntrepriseTypeMilieuStages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EntreprisesTypesMilieuxStage.Include(e => e.Entreprises).Include(e => e.TypesMilieuxStage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EntrepriseTypeMilieuStages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrepriseTypeMilieuStage = await _context.EntreprisesTypesMilieuxStage
                .Include(e => e.Entreprises)
                .Include(e => e.TypesMilieuxStage)
                .FirstOrDefaultAsync(m => m.EntrepriseTypeMilieuStageId == id);
            if (entrepriseTypeMilieuStage == null)
            {
                return NotFound();
            }

            return View(entrepriseTypeMilieuStage);
        }

        // GET: EntrepriseTypeMilieuStages/Create
        public IActionResult Create()
        {
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "EntrepriseId", "NomEntreprise");
            ViewData["TypeMilieuStageId"] = new SelectList(_context.TypesMilieuxStage, "TypeMilieuStageId", "DescriptionTypeMilieuStage");
            return View();
        }

        // POST: EntrepriseTypeMilieuStages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntrepriseTypeMilieuStageId,EntrepriseId,TypeMilieuStageId")] EntrepriseTypeMilieuStage entrepriseTypeMilieuStage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entrepriseTypeMilieuStage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "EntrepriseId", "NomEntreprise", entrepriseTypeMilieuStage.EntrepriseId);
            ViewData["TypeMilieuStageId"] = new SelectList(_context.TypesMilieuxStage, "TypeMilieuStageId", "DescriptionTypeMilieuStage", entrepriseTypeMilieuStage.TypeMilieuStageId);
            return View(entrepriseTypeMilieuStage);
        }

        // GET: EntrepriseTypeMilieuStages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrepriseTypeMilieuStage = await _context.EntreprisesTypesMilieuxStage.FindAsync(id);
            if (entrepriseTypeMilieuStage == null)
            {
                return NotFound();
            }
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "EntrepriseId", "NomEntreprise", entrepriseTypeMilieuStage.EntrepriseId);
            ViewData["TypeMilieuStageId"] = new SelectList(_context.TypesMilieuxStage, "TypeMilieuStageId", "DescriptionTypeMilieuStage", entrepriseTypeMilieuStage.TypeMilieuStageId);
            return View(entrepriseTypeMilieuStage);
        }

        // POST: EntrepriseTypeMilieuStages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntrepriseTypeMilieuStageId,EntrepriseId,TypeMilieuStageId")] EntrepriseTypeMilieuStage entrepriseTypeMilieuStage)
        {
            if (id != entrepriseTypeMilieuStage.EntrepriseTypeMilieuStageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrepriseTypeMilieuStage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntrepriseTypeMilieuStageExists(entrepriseTypeMilieuStage.EntrepriseTypeMilieuStageId))
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
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "EntrepriseId", "NomEntreprise", entrepriseTypeMilieuStage.EntrepriseId);
            ViewData["TypeMilieuStageId"] = new SelectList(_context.TypesMilieuxStage, "TypeMilieuStageId", "DescriptionTypeMilieuStage", entrepriseTypeMilieuStage.TypeMilieuStageId);
            return View(entrepriseTypeMilieuStage);
        }

        // GET: EntrepriseTypeMilieuStages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrepriseTypeMilieuStage = await _context.EntreprisesTypesMilieuxStage
                .Include(e => e.Entreprises)
                .Include(e => e.TypesMilieuxStage)
                .FirstOrDefaultAsync(m => m.EntrepriseTypeMilieuStageId == id);
            if (entrepriseTypeMilieuStage == null)
            {
                return NotFound();
            }

            return View(entrepriseTypeMilieuStage);
        }

        // POST: EntrepriseTypeMilieuStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrepriseTypeMilieuStage = await _context.EntreprisesTypesMilieuxStage.FindAsync(id);
            _context.EntreprisesTypesMilieuxStage.Remove(entrepriseTypeMilieuStage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntrepriseTypeMilieuStageExists(int id)
        {
            return _context.EntreprisesTypesMilieuxStage.Any(e => e.EntrepriseTypeMilieuStageId == id);
        }
    }
}
