using RaioXVegano.entities;
using RaioXVegano.iso;
using RaioXVegano.Util;

namespace RaioXVegano.so.mock
{
    public abstract class BaseBancoDeDadosSO<Request, Response> : IBaseBancoDeDadosSO<Request, Response>
        where Request : IBaseBancoDeDadosRequest
        where Response : IBaseBancoDeDadosResponse
    {
        public abstract Response Executar(Request request);
        
        protected Produto PreencheProdutoSendoEditado()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO,
                Id = 1,
                Ingredientes = "Produto",
                IsVegano = true,
                Nome = "Produto Sendo Editado",
                UsuarioEditando = "ChaveUsuario1"
            };
        }

        protected Produto PreencheProdutoVegano()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_ALTERACAO_SUCESSO,
                Id = 1,
                Ingredientes = "Tudo com amor e sem crueldade",
                IsVegano = true,
                Nome = "Produto com Amor"
            };
        }

        protected Produto PreencheProdutoNaoVegano()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO,
                Id = 2,
                Ingredientes = "Com crueldade =/",
                IsVegano = false,
                Nome = "Produto com Animais =/",
                Motivo = "Tem leite e ovos",
                Base64ImagemProduto = "Base64"
            };
        }
    }
}
