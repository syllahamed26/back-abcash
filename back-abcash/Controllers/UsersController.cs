using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_abcash.Models;
using back_abcash.Models.Entities;
using back_abcash.Dto;

namespace back_abcash.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly AbcashDbContext _context;

        public UsersController(AbcashDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return BadRequest(new { code = "404", message = "user introuvable" });
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<IActionResult> PutUser(int id, User data)
        {
            var user = await _context.Users.FindAsync(id);

            var verifLogin = from u in _context.Users
                             where u.Login == user.Login && u.Id != id
                             select new { u.Id, u.Login };

            if (user == null)
            {
                return BadRequest(new { code = "404", message = "user introuvable" });
            }
            else if (verifLogin.Count() > 0)
            {
                return BadRequest(new { code = "404", message = "login déja existant" });
            }

            _context.Entry(user).State = EntityState.Modified;

            user.Nom = data.Nom;
            user.Prenoms = data.Prenoms;
            user.Contact = data.Contact;
            user.Email = data.Email;
            user.Login = data.Login;
            user.Password = data.Password;
            user.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var VerifRole = await _context.Roles.FindAsync(user.RoleId);
            var verifLogin = from u in _context.Users
                             where u.Login == user.Login
                             select new { u.Id, u.Login };
            if (VerifRole == null)
            {
                return BadRequest(new { code = "404", message = "rôle introuvable" });
            }
            else if (verifLogin.Count() > 0)
            {
                return BadRequest(new { code = "404", message = "login déja existant" });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { code = "404", message = "suppression éffectuée" });
        }

        [HttpGet("enordis/{id}")]
        //Activer, Desactiver un user
        public async Task<ActionResult<User>> EnableOrDisable(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest(new { code = "404", message = "user introuvable" });
            }

            _context.Entry(user).State = EntityState.Modified;

            if (user.Statut)
            {
                user.Statut = false;
            }
            else
            {
                user.Statut = true;
            }
            user.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<User>> Login(DtoUserLogin dtoUserLogin)
        {
            if (dtoUserLogin.Login == String.Empty || dtoUserLogin.Password == string.Empty)
            {
                return BadRequest(new { code = "404", message = "Champs login et mot de passe requis" });
            }

            var userFind = from u in _context.Users
                           where u.Login == dtoUserLogin.Login
                           select new { u.Id, u.Password, u.Statut };

            if (userFind.Count() == 0)
            {
                return BadRequest(new { code = "404", message = "données incorrectes" });
            }
            else if (!userFind.First().Statut) //vérification du statut
            {
                return BadRequest(new { code = "404", message = "Compte inactif, contactez l'administrateur" });
            }
            else if (dtoUserLogin.Password != userFind.First().Password) //verification du password
            {
                return BadRequest(new { code = "404", message = "données incorrectes" });
            }

            //recherche de id contenu dans userFind
            var user = await _context.Users.FindAsync(userFind.First().Id);

            return Ok(user);
        }
    }
}
