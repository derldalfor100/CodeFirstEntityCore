using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeFirstEntityCore.Data;
using CodeFirstEntityCore.Models;

namespace CodeFirstEntityCore.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly MySqlDbContext _mysqlContext;

        public PlayersController(ApplicationDbContext context, MySqlDbContext mysqlContext)
        {
            _context = context;

            _mysqlContext = mysqlContext;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Players.Include(p => p.Team);
            //return View(await applicationDbContext.ToListAsync());
            var mySqlDbContext = _mysqlContext.Players.Include(p => p.Team);
            return View(await mySqlDbContext.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.PlayerId == id);

            var playerMySql = await _mysqlContext.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.PlayerId == id);

            if (player == null || playerMySql == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["TeamName"] = new SelectList(_context.Teams, "TeamName", "TeamName");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,FirstName,LastName,Position,TeamName")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                _mysqlContext.Add(player);
                await _context.SaveChangesAsync();
                await _mysqlContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamName"] = new SelectList(_context.Teams, "TeamName", "TeamName", player.TeamName);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            var playerMySql = await _mysqlContext.Players.FindAsync(id);

            if (player == null || playerMySql == null)
            {
                return NotFound();
            }
            ViewData["TeamName"] = new SelectList(_context.Teams, "TeamName", "TeamName", player.TeamName);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PlayerId,FirstName,LastName,Position,TeamName")] Player player)
        {
            if (id != player.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    _mysqlContext.Update(player);
                    await _context.SaveChangesAsync();
                    await _mysqlContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerId))
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
            ViewData["TeamName"] = new SelectList(_context.Teams, "TeamName", "TeamName", player.TeamName);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.PlayerId == id);

            var playerMySql = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.PlayerId == id);

            if (player == null || playerMySql == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var player = await _context.Players.FindAsync(id);
            _context.Players.Remove(player);
            _mysqlContext.Players.Remove(player);
            await _context.SaveChangesAsync();
            await _mysqlContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(string id)
        {
            return _context.Players.Any(e => e.PlayerId == id) && _mysqlContext.Players.Any(e => e.PlayerId == id);
        }
    }
}
