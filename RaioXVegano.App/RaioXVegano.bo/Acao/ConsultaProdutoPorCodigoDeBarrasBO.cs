using RaioXVegano.entities;
using RaioXVegano.entities.Acao;
using RaioXVegano.entities.App;
using RaioXVegano.entities.Enum;
using RaioXVegano.exception;
using RaioXVegano.ibo.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System.Collections.Generic;

namespace RaioXVegano.bo.Acao
{
    public class ConsultaProdutoPorCodigoDeBarrasBO : BaseAcaoBO<ConsultaProdutoPorCodigoDeBarrasAppRequest, ConsultaProdutoPorCodigoDeBarrasAppResponse, ConsultaProdutoPorCodigoDeBarrasRequest, ConsultaProdutoPorCodigoDeBarrasResponse>, IConsultaProdutoPorCodigoDeBarrasBO
    {
        private readonly IConsultaProdutoPorCodigoDeBarrasSO _so;

        public ConsultaProdutoPorCodigoDeBarrasBO(IConsultaProdutoPorCodigoDeBarrasSO consultaProdutoPorCodigoDeBarrasSO) : base(typeof(ConsultaProdutoPorCodigoDeBarrasBO))
        {
            _so = consultaProdutoPorCodigoDeBarrasSO;
        }

        protected override void ValidaForm(ConsultaProdutoPorCodigoDeBarrasAppRequest requestApp)
        {
            bool existeErro = false;

            if (string.IsNullOrEmpty(requestApp.ChaveUsuarioLogado))
            {
                _log.Info("ChaveUsuarioLogado obrigatório");
                existeErro = true;
            }

            if (string.IsNullOrEmpty(requestApp.CodigoDeBarras))
            {
                _log.Info("CodigoDeBarras obrigatório");
                existeErro = true;
            }

            if (existeErro)
            {
                throw new ValidacaoFormException(new Dictionary<string, string>() { { Consts.ERRO_GENERICO, "Erro Genérico" } });
            }
        }

        protected override ConsultaProdutoPorCodigoDeBarrasRequest ConverteFormEmRequestServico(ConsultaProdutoPorCodigoDeBarrasAppRequest requestApp)
        {
            return new ConsultaProdutoPorCodigoDeBarrasRequest() { CodigoDeBarras = requestApp.CodigoDeBarras, ChaveUsuarioLogado = requestApp.ChaveUsuarioLogado };
        }

        protected override ConsultaProdutoPorCodigoDeBarrasResponse ExecutaServico(ConsultaProdutoPorCodigoDeBarrasRequest requestService)
        {
            return _so.Executa(requestService);
        }

        protected override ConsultaProdutoPorCodigoDeBarrasAppResponse MontaResponseApp(ConsultaProdutoPorCodigoDeBarrasResponse responseService)
        {
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = new ConsultaProdutoPorCodigoDeBarrasAppResponse();

            if (!responseService.IsExecucaoSucesso)
            {
                response.IsProdutoSendoEditado = responseService.ListaErros.Contains((int)CodigoRetorno.PRODUTO_SENDO_EDITADO);

                if (response.IsProdutoSendoEditado)
                {
                    response.IsProdutoEncontrado = true;
                }
                else
                {
                    response.IsProdutoEncontrado = false;
                    response.Mensagens = RetornaErrosParaFormTela(responseService.ListaErros); 
                }
            }
            else
            {
                response.IsProdutoEncontrado = !string.IsNullOrEmpty(responseService.Produto?.Nome);
                response.Produto = response.IsProdutoEncontrado ? responseService.Produto : new Produto();
            }

            return response;
        }

        protected override void GerarLogResponseApp(IBaseResponseApp responseApp)
        {
            ConsultaProdutoPorCodigoDeBarrasAppResponse cppcdbAppResponse = responseApp as ConsultaProdutoPorCodigoDeBarrasAppResponse;
            if (cppcdbAppResponse.Produto != null)
            {
                string temp = cppcdbAppResponse.Produto.Base64ImagemProduto;
                cppcdbAppResponse.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {cppcdbAppResponse.GetType().Name} : { AplicacaoUtil.GetDadosLog(cppcdbAppResponse) } ");
                cppcdbAppResponse.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogResponseApp(responseApp);
            }
        }
    }
}
