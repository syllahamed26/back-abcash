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

<<<<<<< HEAD
            if (caisse == null) return BadRequest("Contract does not exist");
=======
            if (caisse == null)
            {
                return BadRequest(new { code = "404", message = "caisse inexistante" });
            }
>>>>>>> 09fda35a6832292fab8197587b9008ce93303797

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
                return BadRequest(new { code = "404", message = "caisse inexistante" });
            }

<<<<<<< HEAD
=======
            var CheckLibelle = from c in _context.Caisses
                               where c.Libelle == caisse.Libelle && c.Id != id
                               select new { c.Id };

            if (CheckLibelle.Count() > 0)
            {
                return BadRequest(new { code = "400", message = "libelle déja utilisé" });
            }

            _context.Entry(caisse).State = EntityState.Modified;

>>>>>>> 09fda35a6832292fab8197587b9008ce93303797
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
                return BadRequest(new { code = "400", message = "libellé déja utilisé" });
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
                return BadRequest(new { code = "404", message = "caisse inexistante" });
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
                return BadRequest(new { code = "404", message = "caisse inexistante" });
            }

            _caisserepo.Delete(id);
            if (_caisserepo.Complete() < 0) return BadRequest("An Unexpected error happen");

            return Ok(new { code = "200", message = "suppression effectuée" });
        }

<<<<<<< HEAD
        private async Task<ActionResult> CaisseExists(int id)
        {
            var res = await _caisserepo.ExistingCaisse(id);
            return Ok(res);
        }
=======
>>>>>>> 09fda35a6832292fab8197587b9008ce93303797
    }
}
