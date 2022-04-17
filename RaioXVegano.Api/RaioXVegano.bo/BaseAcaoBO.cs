using log4net;
using RaioXVegano.entities;
using RaioXVegano.entities.Enum;
using RaioXVegano.exception;
using RaioXVegano.ibo;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;

namespace RaioXVegano.bo
{
    public abstract class BaseAcaoBO<Request, Response> : IBaseAcaoBO<Request, Response>
        where Request : IBaseAcaoRequest
        where Response : IBaseAcaoResponse
    {
        protected ILog _log;

        public BaseAcaoBO(Type type) 
        {
            _log = LogManager.GetLogger(type);
        }

        public Response Executar(Request request)
        {
            _log.Info("BaseAcaoSO.Executar... entrou");

            GerarLogAcaoRequest(request);

            Response response = Activator.CreateInstance<Response>();

            try
            {
                _log.Info("ValidaRequestEmComum... ");
                ValidaRequestEmComum(request);
                _log.Info("ValidaRequestEmComum... OK");

                _log.Info("Valida... ");
                Valida(request);
                _log.Info("Valida... OK");

                _log.Info("ChamaServico... ");
                response = ChamaServico(request);
                _log.Info("ChamaServico... OK");
            }
            catch (ValicacaoException ve)
            {
                _log.Error(ve);
                response.ListaErros = ve.ListaErros;
            }    
            catch (Exception e)
            {
                _log.Error(e);
                response.ListaErros = new List<int>()
                {
                    { (int)CodigoRetorno.ERRO_GENERICO }
                };
            }

            GerarLogAcaoResponse(response);
            
            _log.Info("BaseAcaoSO.Executar... retornando");

            return response;
        }

        protected virtual void GerarLogAcaoRequest(IBaseAcaoRequest request)
        {
            if (request != null)
            {
                _log.Info($" dados {request.GetType().Name} : { AplicacaoUtil.GetDadosLog(request) } ");
            }
        }

        protected virtual void GerarLogAcaoResponse(IBaseAcaoResponse response)
        {
            _log.Info($" dados {response.GetType().Name} : { AplicacaoUtil.GetDadosLog(response) } ");
        }

        protected virtual void ValidaRequestEmComum(Request request)
        {
            if (request == null)
            {
                _log.Error($"Código Erro {CodigoRetorno.REQUEST_OBRIGATORIO}");
                throw new ValicacaoException((int)CodigoRetorno.ERRO_GENERICO);
            }

            if (string.IsNullOrEmpty(request.ChaveUsuarioLogado))
            {
                _log.Error($"Código Erro {CodigoRetorno.CHAVE_USUARIO_OBRIGATORIO}");
                throw new ValicacaoException((int)CodigoRetorno.ERRO_GENERICO);
            }
        }

        protected abstract void Valida(Request request);

        protected abstract Response ChamaServico(Request request);
    }
}
