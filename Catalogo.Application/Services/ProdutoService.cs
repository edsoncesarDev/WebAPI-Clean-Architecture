using AutoMapper;
using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
        {
            var productsEntity = await _produtoRepository.GetProdutosAsync();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(productsEntity);
        }

        public async Task<ProdutoDTO> GetById(int id)
        {
            var productEntity = await _produtoRepository.GetByIdAsync(id);
            return _mapper.Map<ProdutoDTO>(productEntity);
        }

        public async Task Add(ProdutoDTO produtoDto)
        {
            var productEntity = _mapper.Map<Produto>(produtoDto);
            await _produtoRepository.CreateAsync(productEntity);
        }

        public async Task Update(ProdutoDTO produtoDto)
        {
            var productEntity = _mapper.Map<Produto>(produtoDto);
            await _produtoRepository.UpdateAsync(productEntity);

        }

        public async Task Remove(int id)
        {
            var produtoEntity = await _produtoRepository.GetByIdAsync(id);
            await _produtoRepository.RemoveAsync(produtoEntity);
            
        }
    }
}
