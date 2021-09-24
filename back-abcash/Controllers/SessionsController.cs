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
    [Route("api/v1/sessions")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly AbcashDbContext _context;

        public SessionsController(AbcashDbContext context)
        {
            _context = context;
        }


        // POST: api/Sessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("start")]
        public async Task<ActionResult<Session>> PostSession(Session session)
        {
            var verifUser = _context.Users.Any(o => o.Id == session.UserId);
            var verifCaisse = _context.Caisses.Any(o => o.Id == session.CaisseId);

            if (!verifCaisse || !verifUser)
            {
                return BadRequest();
            }

            var LastSession = from c in _context.Caisses
                              join s in _context.Sessions on c.Id equals s.CaisseId
                              where c.Id == session.CaisseId
                              orderby s.Id descending
                              select new { s.Id, s.Statut };
            if (LastSession.Count()>0)
            {
                if (LastSession.First().Statut)
                {
                    return BadRequest(new { code = "400", message = "une session est déja en cours" });
                }
            }

            session.CodeSession = RandomCode(20) + DateTime.Now.ToString("ddMMyyyy.HHmmss");
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return Ok(session);
        }

        [HttpGet("stop/{id}")]
        public async Task<ActionResult<Session>> StopSession(int id)
        {
            var verifSession = _context.Sessions.Any(o => o.Id == id);

            if (!verifSession)
            {
                return BadRequest(new { code = "404", message = "session inexistante" });
            }

            var session = _context.Sessions.Find(id);
            if(session.Statut)
            {
                session.Statut = false;
            }
            else
            {
                return BadRequest(new { code = "400", message="session déja arrêtée" });
            }
            session.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(session);
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
