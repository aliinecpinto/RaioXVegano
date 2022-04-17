using RaioXVegano.entities.Acao;
using RaioXVegano.entities.App;
using RaioXVegano.entities.Properties;
using RaioXVegano.exception;
using RaioXVegano.ibo.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System.Collections.Generic;
using System.Linq;

namespace RaioXVegano.bo.Acao
{
    public class AtualizaProdutoBO : BaseAcaoBO<AtualizaProdutoAppRequest, AtualizaProdutoAppResponse, AtualizaProdutoRequest, AtualizaProdutoResponse>, IAtualizaProdutoBO
    {
        private readonly IAtualizaProdutoSO _so;
        public AtualizaProdutoBO(IAtualizaProdutoSO so) : base(typeof(AtualizaProdutoBO))
        {
            _so = so;
        }

        protected override void ValidaForm(AtualizaProdutoAppRequest requestApp)
        {
            IDictionary<string, string> msgErro = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(requestApp.ChaveUsuarioLogado))
            {
                _log.Info("ChaveUsuarioLogado obrigatório");
                msgErro.Add(Consts.ERRO_GENERICO, string.Empty);
            }

            if (string.IsNullOrEmpty(requestApp.Produto?.Nome))
            {
                _log.Info("Nome obrigatório");
                msgErro.Add(Consts.NOME_PRODUTO, Resources.campoObrigatorio);
            }

            if (requestApp.Produto?.IsVegano == null)
            {
                _log.Info("Tipo Produto obrigatório");
                msgErro.Add(Consts.TIPO_PRODUTO, Resources.campoObrigatorio);
            }
            else if (!requestApp.Produto.IsVegano.Value && string.IsNullOrEmpty(requestApp.Produto?.Motivo))
            {
                _log.Info("Motivo obrigatório");
                msgErro.Add(Consts.MOTIVO, Resources.campoObrigatorio);
            }

            if (string.IsNullOrEmpty(requestApp.Produto?.CodigoDeBarras))
            {
                _log.Info("CodigoDeBarras obrigatório");
                msgErro.Add(Consts.ERRO_GENERICO, string.Empty);
            }

            if (string.IsNullOrEmpty(requestApp.Produto?.Base64ImagemProduto))
            {
                //TODO : pensar sobre abrir novamente a tela de carregar imagem.
                _log.Info("Base64ImagemProduto obrigatório");
                msgErro.Add(Consts.ERRO_GENERICO, string.Empty);
            }

            if (msgErro.Any())
            {
                throw new ValidacaoFormException(msgErro);
            }
        }

        protected override AtualizaProdutoRequest ConverteFormEmRequestServico(AtualizaProdutoAppRequest requestApp)
        {
            return new AtualizaProdutoRequest() { ChaveUsuarioLogado = requestApp.ChaveUsuarioLogado, Produto = requestApp.Produto };
        }

        protected override AtualizaProdutoResponse ExecutaServico(AtualizaProdutoRequest requestService)
        {
            return _so.Executa(requestService);
        }

        protected override AtualizaProdutoAppResponse MontaResponseApp(AtualizaProdutoResponse responseService)
        {
            AtualizaProdutoAppResponse response = new AtualizaProdutoAppResponse();

            if (!responseService.IsExecucaoSucesso)
            {
                response.Mensagens = RetornaErrosParaFormTela(responseService.ListaErros);
            }

            return response;
        }

        protected override void GerarLogRequestApp(IBaseRequestApp requestApp)
        {
            AtualizaProdutoAppRequest apAppRequest = requestApp as AtualizaProdutoAppRequest;
            if (apAppRequest.Produto != null)
            {
                string temp = apAppRequest.Produto.Base64ImagemProduto;
                apAppRequest.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {apAppRequest.GetType().Name} : { AplicacaoUtil.GetDadosLog(apAppRequest) } ");
                apAppRequest.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogRequestApp(requestApp);
            }
        }
    }
}
