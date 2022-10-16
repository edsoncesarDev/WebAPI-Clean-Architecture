using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await _categoriaService.GetCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var categoria = await _categoriaService.GetById(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategoria([FromBody] CategoriaDTO categoriaDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _categoriaService.Add(categoriaDTO);

            return CreatedAtAction(nameof(GetById), new { id = categoriaDTO.Id }, categoriaDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoria(int id, [FromBody] CategoriaDTO categoriaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(id != categoriaDTO.Id)
                return BadRequest(ModelState);

            await _categoriaService.Update(categoriaDTO);
            return Ok(categoriaDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoria(int id)
        {
            var categoriaDto = await _categoriaService.GetById(id);
            if (categoriaDto == null)
                return NotFound();

            await _categoriaService.Remove(id);
            return Ok(categoriaDto);
        }
    }
}
