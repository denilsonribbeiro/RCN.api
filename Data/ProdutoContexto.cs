using Microsoft.EntityFrameworkCore;

namespace rcn.api.Data
{
    public class ProdutoContexto : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public ProdutoContexto(DbContextOptions<ProdutoContexto> options)
        :base(options)
         { 

         }
    }
}