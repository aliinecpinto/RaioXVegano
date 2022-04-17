using RaioXVegano.entities.BancoDeDados;
using RaioXVegano.entities.Enum;
using RaioXVegano.exception;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.mock.BancoDeDados
{
    public class AtualizaProdutoSO : BaseBancoDeDadosSO<AtualizaProdutoRequest, AtualizaProdutoResponse>, IAtualizaProdutoSO
    {
        /// <summary>
        /// Método responsável por simular a ação de atualizar um produto para testes unitários.
        /// </summary>
        /// <param name="request">
        /// Objeto AtualizaProdutoRequest contendo o código de barras para identificar o retorno correto para o teste unitário.
        /// </param>
        /// <returns>
        /// Se o código de barras for Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO, retorna código de erro 1 (erro genérico).
        /// Se o código de barras for Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO, retorna Exception.
        /// Se o código de barras for Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO e a chave do usuário for diferente do produto sendo editado, retorna ProdutoSendoEditadoException
        /// Se não for nenhum desses casos, retorna código 0 (sucesso).
        /// </returns>
        public override AtualizaProdutoResponse Executar(AtualizaProdutoRequest request)
        {
            AtualizaProdutoResponse response = new AtualizaProdutoResponse() { CodigoRetorno = CodigoRetorno.EXECUCAO_OK };

            if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO.Equals(request.Produto.CodigoDeBarras))
            {
                response.CodigoRetorno = CodigoRetorno.ERRO_GENERICO;
            }
            else if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO.Equals(request.Produto.CodigoDeBarras))
            {
                throw new Exception();
            }
            else if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO.Equals(request.Produto.CodigoDeBarras) && !PreencheProdutoSendoEditado().UsuarioEditando.Equals(request.ChaveUsuarioLogado))
            {
                throw new ProdutoSendoEditadoException();
            }

            return response;
        }
    }
}
