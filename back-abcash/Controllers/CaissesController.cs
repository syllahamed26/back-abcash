using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_abcash.Models;
using back_abcash.Models.Entities;
using back_abcash.Repositories;
using back_abcash.Contrats;

namespace back_abcash.Controllers
{
    [Route("api/v1/caisses")]
    [ApiController]
    public class CaissesController : ControllerBase
    {
        private readonly ICaisseRepository _caisserepo;

        public CaissesController(ICaisseRepository caisseReository) => _caisserepo = caisseReository;

        // GET: api/Caisses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caisse>>> GetCaisses()
        {
            var res = await _caisserepo.GetAll();
            return Ok(res);
        }

        // GET: api/Caisses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Caisse>> GetCaisse(int id)
        {
            var caisse = await _caisserepo.GetById(id);

            if (caisse == null) return BadRequest("Contract does not exist");

            return caisse;
        }

        // PUT: api/Caisses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<IActionResult> PutCaisse(int id, Caisse data)
        {
            var caisse = await _caisserepo.GetById(id);

            if (caisse == null)
            {
                return NotFound();
            }

            caisse.Libelle = data.Libelle;
            caisse.Emplacement = data.Emplacement;
            caisse.UpdatedAt = DateTime.Now;

            _caisserepo.Update(caisse);
            if (_caisserepo.Complete() < 0) return BadRequest("An Unexpected error happen");

            return Ok(caisse);

        }

        // POST: api/Caisses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Caisse>> PostCaisse(Caisse caisse)
        {
            var res = await _caisserepo.GetCaisseByLibelle(caisse.Libelle);

            if (res.Count() > 0)
            {
                return NotFound();
            }

            await _caisserepo.Insert(caisse);
            if (_caisserepo.Complete() < 0) return BadRequest("An Unexpected error happen");

            return CreatedAtAction("GetCaisse", new { id = caisse.Id }, caisse);
        }

        [HttpGet("enordis/{id}")]
        public async Task<ActionResult> EnableOrDisable(int id)
        {
            var caisse = await _caisserepo.GetById(id);

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

             _caisserepo.Update(caisse);
            if (_caisserepo.Complete() < 0) return BadRequest("An Unexpected error happen");

            return Ok(caisse);
        }

        // DELETE: api/Caisses/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteCaisse(int id)
        {
            var caisse = await _caisserepo.GetById(id);

            if (caisse == null)
            {
                return NotFound();
            }

            _caisserepo.Delete(id);
            if (_caisserepo.Complete() < 0) return BadRequest("An Unexpected error happen");

            return Ok("Suppression effectuée");
        }

        private async Task<ActionResult> CaisseExists(int id)
        {
            var res = await _caisserepo.ExistingCaisse(id);
            return Ok(res);
        }
    }
}
