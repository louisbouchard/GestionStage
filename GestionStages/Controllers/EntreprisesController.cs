using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionStages.Data;
using GestionStages.Models.MilieuStage;
using GestionStages.ViewModels.MilieuStage;
using System.Collections;

namespace GestionStages.Controllers
{
    public class EntreprisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntreprisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Entreprises
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Entreprises.Include(e => e.TypesEntreprise);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Entreprises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entreprise = await _context.Entreprises
                .Include(e => e.TypesEntreprise)
                .FirstOrDefaultAsync(m => m.EntrepriseId == id);
            if (entreprise == null)
            {
                return NotFound();
            }

            return View(entreprise);
        }

        // GET: Entreprises/Create
        public IActionResult Create()
        {
            ViewData["TypeEntrepriseId"] = new SelectList(_context.TypesEntreprise, "TypeEntrepriseId", "DescriptionTypeEntreprise");
            return View();
        }

        // POST: Entreprises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntrepriseId,NomEntreprise,AdresseEntreprise,TypeEntrepriseId")] Entreprise entreprise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entreprise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeEntrepriseId"] = new SelectList(_context.TypesEntreprise, "TypeEntrepriseId", "DescriptionTypeEntreprise", entreprise.TypeEntrepriseId);
            return View(entreprise);
        }

        // GET: Entreprises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            EntrepriseModel societe = new EntrepriseModel();

            if (id == null)
            {
                return NotFound();
            }

            var entreprise = await _context.Entreprises.FindAsync(id);
            if (entreprise == null)
            {
                return NotFound();
            }
            ViewData["TypeEntrepriseId"] = new SelectList(_context.TypesEntreprise, "TypeEntrepriseId", "DescriptionTypeEntreprise", entreprise.TypeEntrepriseId);
            societe.Entreprises = entreprise;

            societe.EntreprisesTypesMilieuxStage = _context.EntreprisesTypesMilieuxStage.Include(e => e.Entreprises).Include(e => e.TypesMilieuxStage).ToList();


            return View(societe);
        }

        // POST: Entreprises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntrepriseId,NomEntreprise,AdresseEntreprise,TypeEntrepriseId", Prefix = "Entreprises")] Entreprise entreprise)
        {
            if (id != entreprise.EntrepriseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entreprise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntrepriseExists(entreprise.EntrepriseId))
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
            ViewData["TypeEntrepriseId"] = new SelectList(_context.TypesEntreprise, "TypeEntrepriseId", "DescriptionTypeEntreprise", entreprise.TypeEntrepriseId);
            return View(entreprise);
        }

        // GET: Entreprises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entreprise = await _context.Entreprises
                .Include(e => e.TypesEntreprise)
                .FirstOrDefaultAsync(m => m.EntrepriseId == id);
            if (entreprise == null)
            {
                return NotFound();
            }

            return View(entreprise);
        }

        // POST: Entreprises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entreprise = await _context.Entreprises.FindAsync(id);
            _context.Entreprises.Remove(entreprise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntrepriseExists(int id)
        {
            return _context.Entreprises.Any(e => e.EntrepriseId == id);
        }

        #region Types d'entreprise
        // GET: EntrepriseTypeMilieuStages/Create
        public IActionResult CreateTypeMilieu(string id)
        {
            ViewData["EntrepriseId"] = id; // new SelectList(_context.Entreprises, "EntrepriseId", "NomEntreprise");
            ViewData["TypeMilieuStageId"] = new SelectList(_context.TypesMilieuxStage, "TypeMilieuStageId", "DescriptionTypeMilieuStage");
            return View();
        }

        // POST: EntrepriseTypeMilieuStages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTypeMilieu([Bind("EntrepriseId,TypeMilieuStageId")] EntrepriseTypeMilieuStage entrepriseTypeMilieuStage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entrepriseTypeMilieuStage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "EntrepriseId", "NomEntreprise", entrepriseTypeMilieuStage.EntrepriseId);
            ViewData["TypeMilieuStageId"] = new SelectList(_context.TypesMilieuxStage, "TypeMilieuStageId", "DescriptionTypeMilieuStage", entrepriseTypeMilieuStage.TypeMilieuStageId);
            //return View(entrepriseTypeMilieuStage);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditTypeMilieu(int? id)
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
            ViewData["EntrepriseId"] = id; // new SelectList(_context.Entreprises, "EntrepriseId", "NomEntreprise", entrepriseTypeMilieuStage.EntrepriseId);
            ViewData["TypeMilieuStageId"] = new SelectList(_context.TypesMilieuxStage, "TypeMilieuStageId", "DescriptionTypeMilieuStage", entrepriseTypeMilieuStage.TypeMilieuStageId);
            return View(entrepriseTypeMilieuStage);
        }

        // POST: EntrepriseTypeMilieuStages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTypeMilieu(int id, [Bind("EntrepriseTypeMilieuStageId,EntrepriseId,TypeMilieuStageId")] EntrepriseTypeMilieuStage entrepriseTypeMilieuStage)
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
                    if (!EntrepriseTypeMilieuStageExists(entrepriseTypeMilieuStage.EntrepriseId))
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
            ViewData["EntrepriseId"] = id; // new SelectList(_context.Entreprises, "EntrepriseId", "NomEntreprise", entrepriseTypeMilieuStage.EntrepriseId);
            ViewData["TypeMilieuStageId"] = new SelectList(_context.TypesMilieuxStage, "TypeMilieuStageId", "DescriptionTypeMilieuStage", entrepriseTypeMilieuStage.TypeMilieuStageId);
            return View(entrepriseTypeMilieuStage);
        }

        public async Task<IActionResult> DeleteTypeMilieu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrepriseTypeMilieuStage = await _context.EntreprisesTypesMilieuxStage
                .Include(e => e.Entreprises)
                .Include(e => e.TypesMilieuxStage)
                .FirstOrDefaultAsync(m => m.EntrepriseId == id);
            if (entrepriseTypeMilieuStage == null)
            {
                return NotFound();
            }

            return View(entrepriseTypeMilieuStage);
        }

        // POST: EntrepriseTypeMilieuStages/Delete/5
        //[HttpPost, ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedTypeMilieu(int id, int id2)
        {
            var entrepriseTypeMilieuStage = await _context.EntreprisesTypesMilieuxStage.FindAsync(id, id2);
            _context.EntreprisesTypesMilieuxStage.Remove(entrepriseTypeMilieuStage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntrepriseTypeMilieuStageExists(int id)
        {
            return _context.EntreprisesTypesMilieuxStage.Any(e => e.EntrepriseId == id);
        }
        #endregion Types entreprise
    }
}
