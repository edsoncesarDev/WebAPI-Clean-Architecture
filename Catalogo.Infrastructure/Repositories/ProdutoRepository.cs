using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Repositories
{
    
    public class ProdutoRepository : IProdutoRepository
    {
        readonly ApplicationDbContext _produtoContext;
        public ProdutoRepository(ApplicationDbContext context)
        {
            _produtoContext = context;
        }
        public async Task<Produto> CreateAsync(Produto produto)
        {
            _produtoContext.Add(produto);
            await _produtoContext.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _produtoContext.Produtos.Include(c => c.Categoria).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return await _produtoContext.Produtos.ToListAsync();
        }

        public async Task<Produto> RemoveAsync(Produto produto)
        {
            _produtoContext.Remove(produto);
            await _produtoContext.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            _produtoContext.Update(produto);
            await _produtoContext.SaveChangesAsync();
            return produto;
        }
    }
}
