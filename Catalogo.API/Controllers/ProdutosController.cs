using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
        {
            var produtos = await _produtoService.GetProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetById(int id)
        {
            var produto = await _produtoService.GetById(id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduto([FromBody] ProdutoDTO produtoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _produtoService.Add(produtoDto);

            return CreatedAtAction(nameof(GetById), new { id = produtoDto.Id }, produtoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduto(int id, [FromBody] ProdutoDTO produtoDto)
        {
            if(id != produtoDto.Id)
                return BadRequest();

            await _produtoService.Update(produtoDto);

            return Ok(produtoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduto(int id)
        {
            var produtoDto = await _produtoService.GetById(id);
            if (produtoDto == null)
                return BadRequest();

            await _produtoService.Remove(id);
            return Ok(produtoDto);
        }
    }
}
