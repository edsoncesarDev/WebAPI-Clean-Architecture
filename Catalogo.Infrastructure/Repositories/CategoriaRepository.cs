using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private ApplicationDbContext _categoriaContext;
        public CategoriaRepository(ApplicationDbContext context)
        {
            _categoriaContext = context;
        }
        public async Task<Categoria> CreateAsync(Categoria categoria)
        {
            _categoriaContext.Add(categoria);
            await _categoriaContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            return await _categoriaContext.Categorias.FindAsync(id);
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return await _categoriaContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> RemoveAsync(Categoria categoria)
        {
            _categoriaContext.Remove(categoria);
            await _categoriaContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> UpdateAsync(Categoria categoria)
        {
            _categoriaContext.Update(categoria);
            await _categoriaContext.SaveChangesAsync();
            return categoria;
        }
    }
}
