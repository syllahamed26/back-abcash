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
    [Route("api/affecter")]
    [ApiController]
    public class UserCaissesController : ControllerBase
    {
        private readonly AbcashDbContext _context;

        public UserCaissesController(AbcashDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> addAffectation(UserCaisse userCaisse)
        {
            var verifAffect = from r in _context.UsersCaisses
                              where r.UserId == userCaisse.UserId && r.CaisseId == userCaisse.CaisseId
                              select r;

            if (verifAffect.Count() >0)
            {
                return BadRequest();
            }

            userCaisse.DateAffectation = DateTime.Now;
            await _context.UsersCaisses.AddAsync(userCaisse);
            await _context.SaveChangesAsync();

            return Ok(userCaisse);
        }

        [HttpGet("remove/{id}")]
        public async Task<ActionResult> removeAffectation(int id)
        {
            var affec = await _context.UsersCaisses.FindAsync(id);
            if (affec == null)
            {
                return NotFound();
            }

            _context.UsersCaisses.Remove(affec);
            await _context.SaveChangesAsync();

            return Ok("Suppression effectuée");
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult> affecterUsers(int id)
        {
            var userCheck = await _context.Users.FindAsync(id);
            if (userCheck == null)
            {
                return NotFound();
            }

            var listecaisse = from u in _context.Users
                              join r in _context.UsersCaisses on u.Id equals r.UserId
                              join c in _context.Caisses on r.CaisseId equals c.Id
                              where u.Id == id
                              select new { c.Id, c.Libelle, c.Emplacement, c.CreatedAt, c.UpdatedAt };
                        

            return Ok(listecaisse);
        }

        [HttpGet("caisses/{id}")]
        public async Task<ActionResult> affecterCaisse(int id)
        {
            var caisseCheck = await _context.Caisses.FindAsync(id);
            if (caisseCheck == null)
            {
                return NotFound();
            }

            var listeuser = from u in _context.Users
                              join r in _context.UsersCaisses on u.Id equals r.UserId
                              join c in _context.Caisses on r.CaisseId equals c.Id
                              where c.Id == id
                              select new { u.Id, u.Nom, u.Prenoms, u.Login, u.Email, u.Contact, u.Statut, u.CreatedAt, u.UpdatedAt };


            return Ok(listeuser);
        }
    }
}
