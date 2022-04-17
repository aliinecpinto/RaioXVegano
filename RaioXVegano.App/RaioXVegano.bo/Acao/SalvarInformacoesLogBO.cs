using RaioXVegano.entities.Acao;
using RaioXVegano.entities.App;
using RaioXVegano.entities.Properties;
using RaioXVegano.exception;
using RaioXVegano.ibo.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;

namespace RaioXVegano.bo.Acao
{
    public class SalvarInformacoesLogBO : BaseAcaoBO<SalvarInformacoesLogAppRequest, SalvarInformacoesLogAppResponse, SalvarInformacoesLogRequest, SalvarInformacoesLogResponse>, ISalvarInformacoesLogBO
    {
        private ISalvarInformacoesLogSO _so;

        public SalvarInformacoesLogBO(ISalvarInformacoesLogSO so) : base(typeof(SalvarInformacoesLogBO)) 
        {
            _so = so;
        }

        protected override void ValidaForm(SalvarInformacoesLogAppRequest requestApp)
        {
            if (string.IsNullOrEmpty(requestApp.ChaveUsuarioLogado) || 
                string.IsNullOrEmpty(requestApp.Parametro1) ||
                string.IsNullOrEmpty(requestApp.Parametro2))
            {
                _log.Info("ChaveUsuarioLogado, Parametro1 e Parametro2 obrigatório");
                throw new ValidacaoFormException(new Dictionary<string, string>() { { Consts.ERRO_GENERICO, string.Empty } });
            }
        }

        protected override SalvarInformacoesLogRequest ConverteFormEmRequestServico(SalvarInformacoesLogAppRequest requestApp)
        {
            return new SalvarInformacoesLogRequest() 
            { 
                ChaveUsuarioLogado = requestApp.ChaveUsuarioLogado, 
                Data = DateTime.Now, 
                Parametro1 = requestApp.Parametro1, 
                Parametro2 = requestApp.Parametro2 
            };
        }

        protected override SalvarInformacoesLogResponse ExecutaServico(SalvarInformacoesLogRequest requestService)
        {
            return _so.Executa(requestService);
        }

        protected override SalvarInformacoesLogAppResponse MontaResponseApp(SalvarInformacoesLogResponse responseService)
        {
            SalvarInformacoesLogAppResponse response = new SalvarInformacoesLogAppResponse();

            if (!responseService.IsExecucaoSucesso)
            {
                response.Mensagens = new Dictionary<string, string>() { { string.Empty, string.Format(Resources.erroSalvarLog, Resources.emailRaioXVegano) } };
            }

            return response;
        }
    }
}
