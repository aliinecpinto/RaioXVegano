using FluentNHibernate.Mapping;

namespace RaioXVegano.entities.Mapeamentos
{
    public class ProdutoMap : ClassMap<Produto>
    {
        public ProdutoMap() 
        {
            Table("tb_produtos");
            Id(p => p.Id, "id").GeneratedBy.Identity();
            Map(p => p.CodigoDeBarras, "codigo_barras").Unique().Not.Nullable();
            Map(p => p.Nome, "nome").Not.Nullable();
            Map(p => p.IsVegano, "is_vegano").Not.Nullable();
            Map(p => p.Motivo, "motivo");
            Map(p => p.Ingredientes, "ingredientes");
            Map(p => p.UsuarioEditando, "usuario_editando");
            Map(p => p.Base64ImagemProduto, "base64_imagem_produto").CustomType("StringClob").CustomSqlType("nvarchar(max)");
            Map(p => p.DataAtualizacao, "data_atualizacao");
        }
    }
}
