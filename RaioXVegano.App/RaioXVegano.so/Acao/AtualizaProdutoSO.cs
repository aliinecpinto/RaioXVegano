using RaioXVegano.entities.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.Acao
{
    public class AtualizaProdutoSO : BaseAcaoProdutoSO<AtualizaProdutoRequest, AtualizaProdutoResponse>, IAtualizaProdutoSO
    {
        public AtualizaProdutoSO(Uri baseEndpoint) : base(typeof(AtualizaProdutoSO), baseEndpoint) { }

        protected override void AjustaEndpoint()
        {
            _baseEndpoint = new Uri(_baseEndpoint, "AtualizaProduto");
        }

        protected override AtualizaProdutoResponse ChamaServico(AtualizaProdutoRequest request)
        {
            return ApiClientServiceUtil<AtualizaProdutoRequest, AtualizaProdutoResponse>.Post(_baseEndpoint, request);
        }

        protected override void GerarLogAcaoRequest(IBaseAcaoRequest request)
        {
            AtualizaProdutoRequest apRequest = request as AtualizaProdutoRequest;
            if (!string.IsNullOrEmpty(apRequest?.Produto?.Base64ImagemProduto))
            {
                string temp = apRequest.Produto.Base64ImagemProduto;
                apRequest.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {apRequest.GetType().Name} : { AplicacaoUtil.GetDadosLog(apRequest) } ");
                apRequest.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogAcaoRequest(request);
            }
        }
    }
}
