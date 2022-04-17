using NLog;
using RaioXVegano.entities.Acao;
using RaioXVegano.entities.App;
using RaioXVegano.entities.MapMensagens;
using RaioXVegano.entities.Properties;
using RaioXVegano.exception;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;

namespace RaioXVegano.bo.Acao
{
    public abstract class BaseAcaoBO<RequestApp, ResponseApp, RequestAcao, ResponseAcao> : IBaseAcaoBO<RequestApp, ResponseApp, RequestAcao, ResponseAcao>
        where RequestApp : IBaseRequestApp
        where ResponseApp : IBaseResponseApp
        where RequestAcao : IBaseAcaoRequest
        where ResponseAcao : IBaseAcaoResponse
    {
        protected readonly Logger _log;

        public BaseAcaoBO(Type type)
        {
            _log = LogManager.GetLogger(type.FullName);
        }

        public ResponseApp Executar(RequestApp requestApp)
        {
            _log.Info($"BaseAcaoBO.Executar .. entrou ");

            ResponseApp responseApp = Activator.CreateInstance<ResponseApp>();

            GerarLogRequestApp(requestApp);

            try
            {
                _log.Info("ValidaForm... ");
                ValidaForm(requestApp);
                _log.Info("ValidaForm... OK");

                _log.Info("ConverteFormEmRequestServico... ");
                RequestAcao requestAcao = ConverteFormEmRequestServico(requestApp);
                _log.Info("ConverteFormEmRequestServico... OK");

                _log.Info("ExecutaServico... ");
                ResponseAcao responseAcao = ExecutaServico(requestAcao);
                _log.Info("ExecutaServico... OK");

                _log.Info("MontaResponseApp... ");
                responseApp = MontaResponseApp(responseAcao);
                _log.Info("MontaResponseApp... OK");
            }
            catch (ValidacaoFormException ex)
            {
                _log.Error(ex);
                responseApp.Mensagens = ex.Mensagens;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                responseApp.Mensagens = new Dictionary<string, string>() { { Consts.ERRO_GENERICO, string.Format(Resources.erroGenerico, Resources.emailRaioXVegano) } };
            }

            GerarLogResponseApp(responseApp);

            _log.Info($"BaseAcaoBO.Executar .. retornando ");

            return responseApp;
        }

        protected virtual void GerarLogRequestApp(IBaseRequestApp requestApp)
        {
            _log.Info($" dados {requestApp.GetType().Name} : { AplicacaoUtil.GetDadosLog(requestApp) } ");
        }

        protected virtual void GerarLogResponseApp(IBaseResponseApp responseApp)
        {
            _log.Info($" dados {responseApp.GetType().Name} : { AplicacaoUtil.GetDadosLog(responseApp) } ");
        }

        protected IDictionary<string, string> RetornaErrosParaFormTela(IList<int> errosServico)
        {
            IDictionary<string, string> errosFormTela = new Dictionary<string, string>();

            MapCampoTelaMsgProduto mapCampoTela = MapCampoTelaMsgProduto.Instancia;
            MapMensagens mapMensagens = MapMensagens.Instancia;

            foreach (int idErroServico in errosServico)
            {
                string campoTela = mapCampoTela.MapCampoProduto[idErroServico];
                string msgErro = mapMensagens.MapMensagensApp[idErroServico];
                errosFormTela.Add(campoTela, msgErro);
            }

            return errosFormTela;
        }

        protected abstract void ValidaForm(RequestApp requestApp);
        protected abstract RequestAcao ConverteFormEmRequestServico(RequestApp requestApp);
        protected abstract ResponseAcao ExecutaServico(RequestAcao requestService);
        protected abstract ResponseApp MontaResponseApp(ResponseAcao responseService);
    }
}
