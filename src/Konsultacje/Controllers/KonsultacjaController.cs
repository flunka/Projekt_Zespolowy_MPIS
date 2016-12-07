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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index(bool moje)
        {
            IEnumerable<PrzegladajKonsultacjeViewModel> query;
            if (moje) {
                query = pobierzMoje();
            }
            else {
                query = pobierzWszystkie();
            }
            
            
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
                IdPracownika = pracownik.Id,
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
        [Authorize(Roles = "PracownikUczelni")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Budynek,Sala,Termin,Limit")] Konsultacja konsultacja)
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Budynek,Sala,Termin,Limit,PracownikUczelniID")] Konsultacja konsultacja)
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

        public async Task<IActionResult> Zaproponuj()
        {
            var query = from a in _context.Users.ToList()
                        join b in _context.UserRoles on a.Id equals b.UserId
                        join c in _context.Roles on b.RoleId equals c.Id
                        where c.Name == "PracownikUczelni"
                        select new ApplicationUser{ 
                            DisplayName = a.DisplayName,                           
                            Id = a.Id                            
                        };

            SelectList lista = new SelectList(query.ToList(), "Id", "DisplayName");
            ViewBag.PracownikUczelniID = lista;                 
            return View();
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Zaproponuj([Bind("ID,Termin,Temat,PracownikUczelniID")] PropozycjeKonsultacjiViewModel propozycjaKonsultacji)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home", null);
            }
            return View();
        }




        private bool KonsultacjaExists(int id)
        {
            return _context.Konsultacja.Any(e => e.ID == id);
        }

        private IEnumerable<PrzegladajKonsultacjeViewModel> pobierzWszystkie() {
            var query = from c in _context.Konsultacja.ToList()
                        join d in _context.Users.ToList() on c.PracownikUczelniID equals d.Id
                        select new PrzegladajKonsultacjeViewModel
                        {
                            Id = c.ID,
                            IdPracownika = d.Id,
                            DisplayName = d.DisplayName,
                            Budynek = c.Budynek,
                            Sala = c.Sala,
                            Termin = c.Termin
                        };
            return query;
        }
        private IEnumerable<PrzegladajKonsultacjeViewModel> pobierzMoje()
        {
            var query = from c in _context.Konsultacja.ToList()
                        join d in _context.Users.ToList() on c.PracownikUczelniID equals d.Id
                        where c.PracownikUczelniID == _context.Users.Single(user => user.Id == _userManager.GetUserId(User)).Id
                        select new PrzegladajKonsultacjeViewModel
                        {
                            Id = c.ID,
                            IdPracownika = d.Id,
                            DisplayName = d.DisplayName,
                            Budynek = c.Budynek,
                            Sala = c.Sala,
                            Termin = c.Termin
                        };
            return query;
        }
    }
}
