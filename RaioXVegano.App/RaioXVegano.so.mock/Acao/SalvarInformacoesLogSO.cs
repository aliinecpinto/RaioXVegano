using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System.Collections.Generic;

namespace RaioXVegano.so.mock.Acao
{
    public class SalvarInformacoesLogSO : BaseAcaoSO<SalvarInformacoesLogRequest, SalvarInformacoesLogResponse>, ISalvarInformacoesLogSO
    {
        public override SalvarInformacoesLogResponse Executa(SalvarInformacoesLogRequest request)
        {
            SalvarInformacoesLogResponse response = new SalvarInformacoesLogResponse() { IsExecucaoSucesso = true };

            if (Consts.TESTE_CODIGO_USUARIO_ERRO.Equals(request.ChaveUsuarioLogado))
            {
                response.IsExecucaoSucesso = false;
                response.ListaErros = new List<int>() { { (int)CodigoRetorno.ERRO_GENERICO } };
            }

            return response;
        }
    }
}
