using log4net;
using NHibernate;
using RaioXVegano.entities;
using RaioXVegano.iso;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so
{
    public abstract class BaseBancoDeDadosSO<Request, Response> : IBaseBancoDeDadosSO<Request, Response>
        where Request : IBaseBancoDeDadosRequest
        where Response : IBaseBancoDeDadosResponse
    {

        protected ILog _log;
        protected readonly ISession _sessao;

        public BaseBancoDeDadosSO(Type type, ISession sessao)
        {
            _log = LogManager.GetLogger(type);
            _sessao = sessao;
        }

        public Response Executar(Request request)
        {
            _log.Info("BaseBancoDeDadosSO.Executar... entrou");

            GerarLogAcaoRequest(request);

            _log.Info($"ChamaServico... ");
            Response response = ChamaServico(request);
            _log.Info($"ChamaServico... OK");

            GerarLogAcaoResponse(response);

            _log.Info("BaseBancoDeDadosSO.Executar... retornando");

            return response;
        }

        protected virtual void GerarLogAcaoRequest(IBaseBancoDeDadosRequest request)
        {
            if (request != null)
            {
                _log.Info($" dados {request.GetType().Name} : { AplicacaoUtil.GetDadosLog(request) } ");
            }
        }

        protected virtual void GerarLogAcaoResponse(IBaseBancoDeDadosResponse response)
        {
            if (response != null)
            {
                _log.Info($" dados {response.GetType().Name} : { ParseUtil.ParseJson(response) } ");
            }
        }

        protected abstract Response ChamaServico(Request request);
    }
}
