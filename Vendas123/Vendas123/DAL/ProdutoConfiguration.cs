
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas123.Models;

namespace Vendas123.DAL
{
    internal class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder
                .Property(l => l.Codigo)
                .IsRequired();

            builder
                .Property(l => l.Descricao);

            builder
                .Property(l => l.Preco);

            builder
                .Property(l => l.Quantidade);
        }
    }
}