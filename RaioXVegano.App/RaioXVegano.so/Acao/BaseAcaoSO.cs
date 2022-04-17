using NLog;
using RaioXVegano.entities.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.Acao
{
    public abstract class BaseAcaoSO<Request, Response> : IBaseAcaoSO<Request, Response>
        where Request : IBaseAcaoRequest
        where Response : IBaseAcaoResponse
    {
        protected readonly Logger _log;

        protected Uri _baseEndpoint;

        public BaseAcaoSO(Type type)
        {
            _log = LogManager.GetLogger(type.FullName);
        }

        public Response Executa(Request request)
        {
            _log.Info("BaseAcaoSO.Executar... entrou");

            GerarLogAcaoRequest(request);

            _log.Info($"AjustaEndpoint... ");
            AjustaEndpoint();
            _log.Info($"AjustaEndpoint... OK");

            _log.Info($"ChamaServico... ");
            Response response = ChamaServico(request);
            _log.Info($"ChamaServico... OK");

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
            if (response != null)
            {
                _log.Info($" dados {response.GetType().Name} : { AplicacaoUtil.GetDadosLog(response) } ");
            }
        }

        protected abstract void AjustaEndpoint();
        protected abstract Response ChamaServico(Request request);
    }
}
