using RaioXVegano.entities.BancoDeDados;
using RaioXVegano.entities.Enum;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;

namespace RaioXVegano.so.mock.BancoDeDados
{
    public class SalvarInformacoesLogSO : BaseBancoDeDadosSO<SalvarInformacoesLogRequest, SalvarInformacoesLogResponse>, ISalvarInformacoesLogSO
    {

        /// <summary>
        /// Método responsável por simular a ação de salvar log para testes unitários.
        /// </summary>
        /// <param name="request">
        /// Objeto SalvarInformacoesLogAppRequest contendo o chave do usuário para identificar o retorno correto para o teste unitário.
        /// </param>
        /// <returns>
        /// Se a chave do usuário for Consts.TESTE_CODIGO_USUARIO_ERRO, retorna código de erro 1 (erro genérico).
        /// Se não for nenhum desses casos, retorna código 0 (sucesso).
        /// </returns>
        public override SalvarInformacoesLogResponse Executar(SalvarInformacoesLogRequest request)
        {
            SalvarInformacoesLogResponse response = new SalvarInformacoesLogResponse() { CodigoRetorno = CodigoRetorno.EXECUCAO_OK };

            if (Consts.TESTE_CODIGO_USUARIO_ERRO.Equals(request.Log.ChaveUsuarioLogado))
            {
                response.CodigoRetorno = CodigoRetorno.ERRO_GENERICO;
            }

            return response;
        }
    }
}
