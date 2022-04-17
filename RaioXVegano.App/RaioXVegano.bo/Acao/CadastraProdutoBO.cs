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
    public class CadastraProdutoBO : BaseAcaoBO<CadastraProdutoAppRequest, CadastraProdutoAppResponse, CadastraProdutoRequest, CadastraProdutoResponse>, ICadastraProdutoBO
    {
        private readonly ICadastraProdutoSO _so;

        public CadastraProdutoBO(ICadastraProdutoSO so) : base(typeof(CadastraProdutoBO))
        {
            _so = so;
        }

        protected override void ValidaForm(CadastraProdutoAppRequest requestApp)
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
            else if (!requestApp.Produto.IsVegano.Value && string.IsNullOrEmpty(requestApp.Produto.Motivo))
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

        protected override CadastraProdutoRequest ConverteFormEmRequestServico(CadastraProdutoAppRequest requestApp)
        {
            return new CadastraProdutoRequest() { ChaveUsuarioLogado = requestApp.ChaveUsuarioLogado, Produto = requestApp.Produto };
        }

        protected override CadastraProdutoResponse ExecutaServico(CadastraProdutoRequest requestService)
        {
            return _so.Executa(requestService);
        }

        protected override CadastraProdutoAppResponse MontaResponseApp(CadastraProdutoResponse responseService)
        {
            CadastraProdutoAppResponse response = new CadastraProdutoAppResponse();

            if (!responseService.IsExecucaoSucesso)
            {
                response.Mensagens = RetornaErrosParaFormTela(responseService.ListaErros);
            }

            return response;
        }

        protected override void GerarLogRequestApp(IBaseRequestApp requestApp)
        {
            CadastraProdutoAppRequest cpAppRequest = requestApp as CadastraProdutoAppRequest;
            if (cpAppRequest.Produto != null)
            {
                string temp = cpAppRequest.Produto.Base64ImagemProduto;
                cpAppRequest.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {cpAppRequest.GetType().Name} : { AplicacaoUtil.GetDadosLog(cpAppRequest) } ");
                cpAppRequest.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogRequestApp(requestApp);
            }
        }
    }
}
