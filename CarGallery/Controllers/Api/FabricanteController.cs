using CarGallery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarGallery.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricanteController : ControllerBase
    {
        private readonly CarGalleryContext _context;

        public FabricanteController(CarGalleryContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(this._context.Fabricantes.Include(x => x.Carros).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
         {
            Fabricante fabricante = _context.Fabricantes.Include(x => x.Carros).FirstOrDefault(x => x.Id == id);

            if (fabricante == null)
                return NotFound();

            return Ok(fabricante);

        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Fabricante fabricante)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);


            _context.Fabricantes.Add(fabricante);
            await _context.SaveChangesAsync();
           
            return Created($"/fabricante/{fabricante.Id}", fabricante);
        }

        [HttpDelete("{id}")] 
        public IActionResult Excluir(int id)
        {
            Fabricante fabricante = _context.Fabricantes.FirstOrDefault(x => x.Id == id);

            if (fabricante == null)
                return NotFound();

            _context.Remove(fabricante);
            _context.SaveChanges();

            return Ok();

        }


    }
}
