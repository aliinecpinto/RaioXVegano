using RaioXVegano.entities.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.Acao
{
    public class ConsultaProdutoPorCodigoDeBarrasSO : BaseAcaoProdutoSO<ConsultaProdutoPorCodigoDeBarrasRequest, ConsultaProdutoPorCodigoDeBarrasResponse>, IConsultaProdutoPorCodigoDeBarrasSO
    {
        public ConsultaProdutoPorCodigoDeBarrasSO(Uri baseEndpoint) : base(typeof(ConsultaProdutoPorCodigoDeBarrasSO), baseEndpoint) { }

        protected override void AjustaEndpoint()
        {
            _baseEndpoint = new Uri(_baseEndpoint, "ConsultaProdutoPorCodigoDeBarras");
        }

        protected override ConsultaProdutoPorCodigoDeBarrasResponse ChamaServico(ConsultaProdutoPorCodigoDeBarrasRequest request)
        {
            return ApiClientServiceUtil<ConsultaProdutoPorCodigoDeBarrasRequest, ConsultaProdutoPorCodigoDeBarrasResponse>.Post(_baseEndpoint, request);
        }

        protected override void GerarLogAcaoResponse(IBaseAcaoResponse response)
        {
            ConsultaProdutoPorCodigoDeBarrasResponse cpResponse = response as ConsultaProdutoPorCodigoDeBarrasResponse;
            if (!string.IsNullOrEmpty(cpResponse?.Produto?.Base64ImagemProduto))
            {
                string temp = cpResponse.Produto.Base64ImagemProduto;
                cpResponse.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {cpResponse.GetType().Name} : { AplicacaoUtil.GetDadosLog(cpResponse) } ");
                cpResponse.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogAcaoResponse(response);
            }
        }
    }
}
