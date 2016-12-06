using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Konsultacje.Data;
using Konsultacje.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Konsultacje.Models.KonsultacjaViewModel;

namespace Konsultacje.Controllers
{
    public class KonsultacjaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public KonsultacjaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Konsultacjas
        public async Task<IActionResult> Index()
        {
            var query = from c in _context.Konsultacja.ToList()
                        join d in _context.Users.ToList() on c.PracownikUczelniID equals d.Id
                        select new PrzegladajKonsultacjeViewModel
                        {
                            Id = c.ID,
                            DisplayName = d.DisplayName,
                            Budynek = c.Budynek,
                            Sala = c.Sala,
                            Termin = c.Termin
                        };
            
            return View(query);
        }

        public async Task<IActionResult> Zapis(int ID)
        {           
            return RedirectToAction("Create", "ZapisNaKonsultacje", new { id = ID });
        }

        // GET: Konsultacjas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultacja = await _context.Konsultacja.SingleOrDefaultAsync(m => m.ID == id);
            var pracownik = await _context.Users.SingleOrDefaultAsync(m => m.Id == konsultacja.PracownikUczelniID);
            var model = new PrzegladajKonsultacjeViewModel()
            {
                Id = konsultacja.ID,
                DisplayName = pracownik.DisplayName,
                Budynek = konsultacja.Budynek,
                Sala = konsultacja.Sala,
                Termin = konsultacja.Termin
            };
            if (konsultacja == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Konsultacjas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Konsultacjas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Budynek,Sala,Termin")] Konsultacja konsultacja)
        {
            if (ModelState.IsValid)
            {
                var pracownik = _context.Users.Single(user => user.Id == _userManager.GetUserId(User));
                konsultacja.PracownikUczelni = pracownik;
                _context.Add(konsultacja);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(konsultacja);
        }

        // GET: Konsultacjas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultacja = await _context.Konsultacja.SingleOrDefaultAsync(m => m.ID == id);
            if (konsultacja == null)
            {
                return NotFound();
            }
            return View(konsultacja);
        }

        // POST: Konsultacjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Budynek,Sala,Termin")] Konsultacja konsultacja)
        {
            if (id != konsultacja.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konsultacja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KonsultacjaExists(konsultacja.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(konsultacja);
        }

        // GET: Konsultacjas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultacja = await _context.Konsultacja.SingleOrDefaultAsync(m => m.ID == id);
            if (konsultacja == null)
            {
                return NotFound();
            }

            return View(konsultacja);
        }

        // POST: Konsultacjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var konsultacja = await _context.Konsultacja.SingleOrDefaultAsync(m => m.ID == id);
            _context.Konsultacja.Remove(konsultacja);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KonsultacjaExists(int id)
        {
            return _context.Konsultacja.Any(e => e.ID == id);
        }
    }
}
