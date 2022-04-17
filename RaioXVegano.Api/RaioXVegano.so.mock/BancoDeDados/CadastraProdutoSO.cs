using RaioXVegano.entities.BancoDeDados;
using RaioXVegano.entities.Enum;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.mock.BancoDeDados
{
    public class CadastraProdutoSO : BaseBancoDeDadosSO<CadastraProdutoRequest, CadastraProdutoResponse>, ICadastraProdutoSO
    {

        /// <summary>
        /// Método responsável por simular a ação de cadastrar um produto para testes unitários.
        /// </summary>
        /// <param name="request">
        /// Objeto CadastraProdutoRequest contendo o código de barras para identificar o retorno correto para o teste unitário.
        /// </param>
        /// <returns>
        /// Se o código de barras for Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO, retorna código de erro 1 (erro genérico).
        /// Se o código de barras for Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO, retorna Exception.
        /// Se não for nenhum desses casos, retorna código 0 (sucesso).
        /// </returns>
        public override CadastraProdutoResponse Executar(CadastraProdutoRequest request)
        {
            CadastraProdutoResponse response = new CadastraProdutoResponse() { CodigoRetorno = CodigoRetorno.EXECUCAO_OK };

            if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO.Equals(request.Produto.CodigoDeBarras))
            {
                response.CodigoRetorno = CodigoRetorno.ERRO_GENERICO;
            }
            else if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO.Equals(request.Produto.CodigoDeBarras))
            {
                throw new Exception();
            }

            return response;
        }
    }
}
