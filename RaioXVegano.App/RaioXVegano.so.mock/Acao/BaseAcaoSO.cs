using RaioXVegano.entities;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;

namespace RaioXVegano.so.mock.Acao
{
    public abstract class BaseAcaoSO<Request, Response> : IBaseAcaoSO<Request, Response>
    {
        public abstract Response Executa(Request request);

        protected Produto PreencheProdutoVegano()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_ALTERACAO_SUCESSO,
                Id = 1,
                Ingredientes = "Tudo com amor e sem crueldade",
                IsVegano = true,
                Nome = "Requeisoja"
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
                Nome = "Qualy",
                Motivo = "Tem leite e ovos"
            };
        }
    }
}
