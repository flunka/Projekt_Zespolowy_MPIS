using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Konsultacje.Data;
using Konsultacje.Models;
using Microsoft.AspNetCore.Identity;
using Konsultacje.Models.KonsultacjaViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Konsultacje.Controllers
{
    public class PropozycjaKonsultacjiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PropozycjaKonsultacjiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;  
        }

        // GET: PropozycjaKonsultacjis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PropozycjaKonsultacji.Include(p => p.PracownikUczelni).Include(p => p.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropozycjaKonsultacjis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propozycjaKonsultacji = await _context.PropozycjaKonsultacji.SingleOrDefaultAsync(m => m.ID == id);
            if (propozycjaKonsultacji == null)
            {
                return NotFound();
            }
            propozycjaKonsultacji.PracownikUczelni = await _context.Users.SingleOrDefaultAsync(m => m.Id == propozycjaKonsultacji.PracownikUczelniID);
            propozycjaKonsultacji.Student = await _context.Users.SingleOrDefaultAsync(m => m.Id == propozycjaKonsultacji.StudentID);


            return View(propozycjaKonsultacji);
        }

        // GET: PropozycjaKonsultacjis/Create
        [Authorize(Roles = "Student")]
        public IActionResult Create()
        {             
            ViewBag.PracownikUczelniID = new SelectList(pobierzPracownikowUczelni().ToList(), "Id", "DisplayName");
            return View();
        }

        // POST: PropozycjaKonsultacjis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PracownikUczelniID,Temat,Termin")] PropozycjaKonsultacji propozycjaKonsultacji)
        {
            if (ModelState.IsValid)
            {
                propozycjaKonsultacji.Student = _context.Users.Single(user => user.Id == _userManager.GetUserId(User));
                _context.Add(propozycjaKonsultacji);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PracownikUczelniID = new SelectList(pobierzPracownikowUczelni().ToList(), "Id", "DisplayName");
            return View(propozycjaKonsultacji);
        }

        // GET: PropozycjaKonsultacjis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propozycjaKonsultacji = await _context.PropozycjaKonsultacji.SingleOrDefaultAsync(m => m.ID == id);
            if (propozycjaKonsultacji == null)
            {
                return NotFound();
            }
            ViewData["PracownikUczelniID"] = new SelectList(_context.Users, "Id", "Id", propozycjaKonsultacji.PracownikUczelniID);
            ViewData["StudentID"] = new SelectList(_context.Users, "Id", "Id", propozycjaKonsultacji.StudentID);
            return View(propozycjaKonsultacji);
        }

        // POST: PropozycjaKonsultacjis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PracownikUczelniID,StudentID,Temat,Termin")] PropozycjaKonsultacji propozycjaKonsultacji)
        {
            if (id != propozycjaKonsultacji.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propozycjaKonsultacji);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropozycjaKonsultacjiExists(propozycjaKonsultacji.ID))
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
            ViewData["PracownikUczelniID"] = new SelectList(_context.Users, "Id", "Id", propozycjaKonsultacji.PracownikUczelniID);
            ViewData["StudentID"] = new SelectList(_context.Users, "Id", "Id", propozycjaKonsultacji.StudentID);
            return View(propozycjaKonsultacji);
        }
                

        // GET: PropozycjaKonsultacjis/Delete/5
        public async Task<IActionResult> Odrzuc(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var propozycjaKonsultacji = await _context.PropozycjaKonsultacji.SingleOrDefaultAsync(m => m.ID == id);
            if (propozycjaKonsultacji == null)
            {
                return NotFound();
            }
            propozycjaKonsultacji.PracownikUczelni = await _context.Users.SingleOrDefaultAsync(m => m.Id == propozycjaKonsultacji.PracownikUczelniID);
            propozycjaKonsultacji.Student = await _context.Users.SingleOrDefaultAsync(m => m.Id == propozycjaKonsultacji.StudentID);


            return View(propozycjaKonsultacji);
        }

        // POST: PropozycjaKonsultacjis/Delete/5
        [HttpPost, ActionName("Odrzuc")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OdrzuceniePotwierdzone(int id)
        {
            var propozycjaKonsultacji = await _context.PropozycjaKonsultacji.SingleOrDefaultAsync(m => m.ID == id);
            _context.PropozycjaKonsultacji.Remove(propozycjaKonsultacji);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: PropozycjaKonsultacjis/Delete/5
        [Authorize(Roles = "PracownikUczelni")]
        public async Task<IActionResult> Akceptuj(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propozycjaKonsultacji = await _context.PropozycjaKonsultacji.SingleOrDefaultAsync(m => m.ID == id);
            
            if (propozycjaKonsultacji == null)
            {
                return NotFound();
            }
            propozycjaKonsultacji.PracownikUczelni = await _context.Users.SingleOrDefaultAsync(m => m.Id == propozycjaKonsultacji.PracownikUczelniID);
            propozycjaKonsultacji.Student = await _context.Users.SingleOrDefaultAsync(m => m.Id == propozycjaKonsultacji.StudentID);

            PropozycjeKonsultacjiViewModel model = new PropozycjeKonsultacjiViewModel
            {
                Id = propozycjaKonsultacji.ID,
                DisplayName = propozycjaKonsultacji.Student.DisplayName,
                IdStudenta = propozycjaKonsultacji.StudentID,
                DispalyName2 = propozycjaKonsultacji.PracownikUczelni.DisplayName,
                IdPracownika = propozycjaKonsultacji.PracownikUczelniID,
                Termin = propozycjaKonsultacji.Termin,
                Temat = propozycjaKonsultacji.Temat
            };

            return View(model);
        }

        // POST: PropozycjaKonsultacjis/Delete/5
        [Authorize(Roles = "PracownikUczelni")]
        [HttpPost, ActionName("Akceptuj")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AkceptacjaPotwierdzona([Bind("Id,IdStudenta,IdPracownika,Temat,Termin,Budynek,Sala")] PropozycjeKonsultacjiViewModel propozycjaKonsultacji)
        {
            var konsultacja = new Konsultacja {
                PracownikUczelniID = propozycjaKonsultacji.IdPracownika,
                Termin = propozycjaKonsultacji.Termin,
                Sala = propozycjaKonsultacji.Sala,
                Budynek = propozycjaKonsultacji.Budynek,
                Limit = 1
            };
            _context.Add(konsultacja);
            var zapis = new ZapisNaKonsultacje {
                Temat = propozycjaKonsultacji.Temat,
                StudentID = propozycjaKonsultacji.IdStudenta,
                KonsultacjaID = konsultacja.ID
            };
            _context.Add(zapis);
            var propozycja = await _context.PropozycjaKonsultacji.SingleOrDefaultAsync(m => m.ID == propozycjaKonsultacji.Id);
            _context.PropozycjaKonsultacji.Remove(propozycja);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PropozycjaKonsultacjiExists(int id)
        {
            return _context.PropozycjaKonsultacji.Any(e => e.ID == id);
        }

        private IEnumerable<ApplicationUser> pobierzPracownikowUczelni() {
            var query = from a in _context.Users.ToList()
                        join b in _context.UserRoles on a.Id equals b.UserId
                        join c in _context.Roles on b.RoleId equals c.Id
                        where c.Name == "PracownikUczelni"
                        select new ApplicationUser
                        {
                            DisplayName = a.DisplayName,
                            Id = a.Id
                        };

            return query;
        }
    }
}
