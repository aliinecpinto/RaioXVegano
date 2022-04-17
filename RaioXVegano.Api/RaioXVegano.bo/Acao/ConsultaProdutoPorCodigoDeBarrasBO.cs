using RaioXVegano.entities;
using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.exception;
using RaioXVegano.ibo.Acao;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;
using System.Collections.Generic;

namespace RaioXVegano.bo.Acao
{
    public class ConsultaProdutoPorCodigoDeBarrasBO : BaseAcaoBO<ConsultaProdutoPorCodigoDeBarrasRequest, ConsultaProdutoPorCodigoDeBarrasResponse>, IConsultaProdutoPorCodigoDeBarrasBO
    {
        private readonly IConsultaProdutoPorCodigoDeBarrasSO _so;

        public ConsultaProdutoPorCodigoDeBarrasBO(IConsultaProdutoPorCodigoDeBarrasSO so) : base(typeof(ConsultaProdutoPorCodigoDeBarrasBO))
        {
            _so = so;
        }

        protected override void Valida(ConsultaProdutoPorCodigoDeBarrasRequest request)
        {
            if (string.IsNullOrEmpty(request.CodigoDeBarras))
            {
                _log.Error($"Código Erro {CodigoRetorno.CODIGO_BARRAS_OBRIGATORIO}");
                throw new ValicacaoException((int)CodigoRetorno.ERRO_GENERICO);
            }
        }

        protected override ConsultaProdutoPorCodigoDeBarrasResponse ChamaServico(ConsultaProdutoPorCodigoDeBarrasRequest request)
        {
            ConsultaProdutoPorCodigoDeBarrasResponse response = new ConsultaProdutoPorCodigoDeBarrasResponse();

            try
            {
                entities.BancoDeDados.ConsultaProdutoPorCodigoDeBarrasRequest bdRequest = new entities.BancoDeDados.ConsultaProdutoPorCodigoDeBarrasRequest()
                {
                    CodigoDeBarras = request.CodigoDeBarras,
                    ChaveUsuarioLogado = request.ChaveUsuarioLogado
                };

                entities.BancoDeDados.ConsultaProdutoPorCodigoDeBarrasResponse bdResponse = _so.Executar(bdRequest);

                if (CodigoRetorno.ERRO_GENERICO == bdResponse.CodigoRetorno)
                {
                    response.IsExecucaoSucesso = false;
                    response.ListaErros = new List<int>() { { (int)CodigoRetorno.ERRO_CONSULTAR_PRODUTO } };
                }
                else
                {
                    response.IsExecucaoSucesso = true;
                    response.Produto = bdResponse.Produto; 
                }
            }
            catch (ProdutoSendoEditadoException)
            {
                response.IsExecucaoSucesso = false;
                response.ListaErros = new List<int>() { { (int)CodigoRetorno.PRODUTO_SENDO_EDITADO } };
            }
            
            return response;
        }

        protected override void GerarLogAcaoResponse(IBaseAcaoResponse response)
        {
            ConsultaProdutoPorCodigoDeBarrasResponse cppcdbResponse = response as ConsultaProdutoPorCodigoDeBarrasResponse;
            if (cppcdbResponse.Produto != null)
            {
                string temp = cppcdbResponse.Produto.Base64ImagemProduto;
                cppcdbResponse.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {cppcdbResponse.GetType().Name} : { AplicacaoUtil.GetDadosLog(cppcdbResponse) } ");
                cppcdbResponse.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogAcaoResponse(response);
            }
        }
    }
}
