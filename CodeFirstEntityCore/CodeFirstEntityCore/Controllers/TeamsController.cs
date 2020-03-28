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
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly MySqlDbContext _mysqlContext;

        public TeamsController(ApplicationDbContext context, MySqlDbContext mysqlContext)
        {
            _context = context;

            _mysqlContext = mysqlContext;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Teams.ToListAsync());
            return View(await _mysqlContext.Teams.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.TeamName == id);

            var teamMySql = await _mysqlContext.Teams
                .FirstOrDefaultAsync(m => m.TeamName == id);

            if (team == null || teamMySql == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamName,City,Province,Country")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                _mysqlContext.Add(team);
                await _context.SaveChangesAsync();
                await _mysqlContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            var teamMySql = await _mysqlContext.Teams.FindAsync(id);

            if (team == null || teamMySql == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TeamName,City,Province,Country")] Team team)
        {
            if (id != team.TeamName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    _mysqlContext.Update(team);
                    await _context.SaveChangesAsync();
                    await _mysqlContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamName))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.TeamName == id);
            var teamMySql = await _mysqlContext.Teams
                .FirstOrDefaultAsync(m => m.TeamName == id);

            if (team == null || teamMySql == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            _mysqlContext.Teams.Remove(team);
            await RemovePlayersFromTeam(team.TeamName);
            await _context.SaveChangesAsync();
            await _mysqlContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(string id)
        {
            return _context.Teams.Any(e => e.TeamName == id) && _mysqlContext.Teams.Any(e => e.TeamName == id);
        }

        private async Task RemovePlayersFromTeam(string id)
        {
            var players = _context.Players.Where(x => x.TeamName == id).ToList();

            if(players.Count == 0)
            {
                return;
            }

            players.ForEach(p =>
            {
                _context.Players.Remove(p);
                _mysqlContext.Players.Remove(p);
            });

            await _context.SaveChangesAsync();
            await _mysqlContext.SaveChangesAsync();

        }
    }
}
