using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_abcash.Models;
using back_abcash.Models.Entities;

namespace back_abcash.Controllers
{
    [Route("api/v1/caisses")]
    [ApiController]
    public class CaissesController : ControllerBase
    {
        private readonly AbcashDbContext _context;

        public CaissesController(AbcashDbContext context)
        {
            _context = context;
        }

        // GET: api/Caisses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caisse>>> GetCaisses()
        {
            return await _context.Caisses.ToListAsync();
        }

        // GET: api/Caisses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Caisse>> GetCaisse(int id)
        {
            var caisse = await _context.Caisses.FindAsync(id);

            if (caisse == null)
            {
                return NotFound();
            }

            return caisse;
        }

        // PUT: api/Caisses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<IActionResult> PutCaisse(int id, Caisse data)
        {
            var caisse = await _context.Caisses.FindAsync(id);

            if (caisse == null)
            {
                return NotFound();
            }

            var CheckLibelle = from c in _context.Caisses
                               where c.Libelle == caisse.Libelle && c.Id != id
                               select new { c.Id };

            if (CheckLibelle.Count() > 0)
            {
                return NotFound();
            }

            _context.Entry(caisse).State = EntityState.Modified;

            caisse.Libelle = data.Libelle;
            caisse.Emplacement = data.Emplacement;
            caisse.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(caisse);

        }

        // POST: api/Caisses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Caisse>> PostCaisse(Caisse caisse)
        {
            var CheckLibelle = from c in _context.Caisses
                               where c.Libelle == caisse.Libelle
                               select new { c.Id };

            if (CheckLibelle.Count() > 0)
            {
                return NotFound();
            }

            _context.Caisses.Add(caisse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaisse", new { id = caisse.Id }, caisse);
        }

        [HttpGet("enordis/{id}")]
        public async Task<ActionResult> EnableOrDisable(int id)
        {
            var caisse = await _context.Caisses.FindAsync(id);

            if (caisse == null)
            {
                return NotFound();
            }

            if (caisse.Statut)
            {
                caisse.Statut = false;
            }
            else
            {
                caisse.Statut = true;
            }

            await _context.SaveChangesAsync();

            return Ok(caisse);
        }

        // DELETE: api/Caisses/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteCaisse(int id)
        {
            var caisse = await _context.Caisses.FindAsync(id);

            if (caisse == null)
            {
                return NotFound();
            }

            _context.Caisses.Remove(caisse);
            await _context.SaveChangesAsync();

            return Ok("Suppression effectuée");
        }

        private bool CaisseExists(int id)
        {
            return _context.Caisses.Any(e => e.Id == id);
        }

        public static string RandomCode(int length)
        {
            string allowed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(allowed
            .OrderBy(o => Guid.NewGuid())
            .Take(length)
            .ToArray());
        }
    }
}
