using RaioXVegano.entities.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.Acao
{
    public class CadastraProdutoSO : BaseAcaoProdutoSO<CadastraProdutoRequest, CadastraProdutoResponse>, ICadastraProdutoSO
    {
        public CadastraProdutoSO(Uri baseEndpoint) : base(typeof(CadastraProdutoSO), baseEndpoint) { }

        protected override void AjustaEndpoint()
        {
            _baseEndpoint = new Uri(_baseEndpoint, "CadastraProduto");
        }

        protected override CadastraProdutoResponse ChamaServico(CadastraProdutoRequest request)
        {
            return ApiClientServiceUtil<CadastraProdutoRequest, CadastraProdutoResponse>.Post(_baseEndpoint, request);
        }

        protected override void GerarLogAcaoRequest(IBaseAcaoRequest request)
        {
            CadastraProdutoRequest cpRequest = request as CadastraProdutoRequest;
            if (!string.IsNullOrEmpty(cpRequest?.Produto?.Base64ImagemProduto))
            {
                string temp = cpRequest.Produto.Base64ImagemProduto;
                cpRequest.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {cpRequest.GetType().Name} : { AplicacaoUtil.GetDadosLog(cpRequest) } ");
                cpRequest.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogAcaoRequest(request);
            }
        }
    }
}
