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
    [Route("api/v1/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AbcashDbContext _context;

        public RolesController(AbcashDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return BadRequest(new { code = "404", message = "role introuvable" });
            }

            return Ok(role);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<IActionResult> PutRole(int id, Role data)
        {
            var role = await _context.Roles.FindAsync(id);

            if(role == null)
            {
                return BadRequest(new { code = "404", message = "role introuvable" });
            }

            role.Libelle = data.Libelle;
            role.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(role);
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return Ok(role);
        }

        // DELETE: api/Roles/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return BadRequest(new { code = "404", message = "role introuvable" });
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(new { code = "200", message = "suppression éffectuée" });
        }
    }
}
