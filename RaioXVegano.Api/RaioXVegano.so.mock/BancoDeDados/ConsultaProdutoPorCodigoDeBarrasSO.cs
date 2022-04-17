using RaioXVegano.entities.BancoDeDados;
using RaioXVegano.entities.Enum;
using RaioXVegano.exception;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.mock.BancoDeDados
{
    public class ConsultaProdutoPorCodigoDeBarrasSO : BaseBancoDeDadosSO<ConsultaProdutoPorCodigoDeBarrasRequest, ConsultaProdutoPorCodigoDeBarrasResponse>, IConsultaProdutoPorCodigoDeBarrasSO
    {

        /// <summary>
        /// Método responsável por simular a ação de consultar um produto por código de barras para testes unitários.
        /// </summary>
        /// <param name="request">
        /// Objeto ConsultaProdutoPorCodigoDeBarrasRequest contendo o código de barras do produto para identificar o retorno correto para o teste unitário.
        /// </param>
        /// <returns>
        /// Se o código de barras for Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO, retorna código de erro 1 (erro genérico).
        /// Se o código de barras for Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO, retorna Exception.
        /// Se o código de barras for Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO e a chave do usuário for diferente do produto sendo editado, retorna ProdutoSendoEditadoException
        /// Se o código de barras for Consts.TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO, retorna código 0 (sucesso) com produto não vegano preenchido.
        /// Se não for nenhum desses casos, retorna código 0 (sucesso) com produto vegano preenchido.
        /// </returns>
        public override ConsultaProdutoPorCodigoDeBarrasResponse Executar(ConsultaProdutoPorCodigoDeBarrasRequest request)
        {
            ConsultaProdutoPorCodigoDeBarrasResponse response = new ConsultaProdutoPorCodigoDeBarrasResponse() { CodigoRetorno = CodigoRetorno.EXECUCAO_OK };

            response.Produto = PreencheProdutoVegano();

            if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO.Equals(request.CodigoDeBarras))
            {
                response.CodigoRetorno = CodigoRetorno.ERRO_GENERICO;
            }
            else if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO.Equals(request.CodigoDeBarras))
            {
                throw new Exception();
            }
            else if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO.Equals(request.CodigoDeBarras) && !PreencheProdutoSendoEditado().UsuarioEditando.Equals(request.ChaveUsuarioLogado))
            {
                throw new ProdutoSendoEditadoException();
            }
            else if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO.Equals(request.CodigoDeBarras))
            {
                response.Produto = PreencheProdutoNaoVegano();
            }
            else if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_CADATRO_SUCESSO.Equals(request.CodigoDeBarras))
            {
                response.Produto = null;
            }

            return response;
        }
    }
}
