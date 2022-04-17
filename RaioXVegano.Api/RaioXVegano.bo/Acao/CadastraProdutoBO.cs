using RaioXVegano.entities;
using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.exception;
using RaioXVegano.ibo.Acao;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;
using System.Collections.Generic;
using System.Linq;

namespace RaioXVegano.bo.Acao
{
    public class CadastraProdutoBO : BaseAcaoBO<CadastraProdutoRequest, CadastraProdutoResponse>, ICadastraProdutoBO
    {
        private readonly ICadastraProdutoSO _so;

        public CadastraProdutoBO(ICadastraProdutoSO so) : base(typeof(CadastraProdutoBO))
        {
            _so = so;
        }

        /// <summary>
        /// Método responsável por validar o produto para cadastro da seguinte forma:
        ///     - Se a classe de produto do request estiver null, retornar código 1 (erro genérico)
        ///     - Campos obrigatórios:
        ///         - CodigoDeBarras retorna código 6.
        ///         - Nome retorna código 7.
        ///         - Motivo retorna código 8 (Apenas se o produto não for vegano).
        /// </summary>
        /// <param name="request">Objeto do tipo CadastraProdutoRequest contendo objeto de produto para validação</param>
        protected override void Valida(CadastraProdutoRequest request)
        {
            if (request.Produto == null)
            {
                _log.Error($"Código Erro {CodigoRetorno.PRODUTO_OBRIGATORIO}");
                throw new ValicacaoException((int)CodigoRetorno.ERRO_GENERICO);
            } 
            else
            {
                IList<int> listaErros = new List<int>();

                if (string.IsNullOrEmpty(request.Produto.CodigoDeBarras))
                {
                    _log.Error($"Código Erro {CodigoRetorno.CODIGO_BARRAS_OBRIGATORIO}");
                    listaErros.AddIfDoesntExists((int)CodigoRetorno.ERRO_GENERICO);
                }

                if (string.IsNullOrEmpty(request.Produto.Nome))
                {
                    listaErros.AddIfDoesntExists((int)CodigoRetorno.NOME_OBRIGATORIO);
                }

                if (!request.Produto.IsVegano && string.IsNullOrEmpty(request.Produto.Motivo))
                {
                    listaErros.AddIfDoesntExists((int)CodigoRetorno.MOTIVO_OBRIGATORIO);
                }

                if (listaErros.Any())
                {
                    throw new ValicacaoException(listaErros);
                }
            }
        }

        /// <summary>
        /// Método responsável por:
        ///     1) Chamar o método AjustaProdutoParaCadastro para preparar o request para o serviço.
        ///     2) Chamar o SO de cadastro do produto.
        /// </summary>
        /// <param name="request">
        /// Objeto do tipo CadastraProdutoRequest contendo objeto de produto para cadastro.
        /// </param>
        /// <returns>
        /// Verificar se o response retornado do SO está com o CodigoRetorno == 1 (erro genérico):
        ///     - Se estiver retorna o campo IsExecucaoSucesso = false e a ListaErros com 
        ///       código 11 (Erro genérico de cadastro de produto).
        ///     - Se não estiver retorna o campo IsExecucaoSucesso = true.
        /// </returns>
        protected override CadastraProdutoResponse ChamaServico(CadastraProdutoRequest request)
        {
            CadastraProdutoResponse response = new CadastraProdutoResponse() { IsExecucaoSucesso = true };
            entities.BancoDeDados.CadastraProdutoRequest bdRequest = AjustaProdutoParaCadastro(request);
            entities.BancoDeDados.CadastraProdutoResponse bdResponse = _so.Executar(bdRequest);

            if (CodigoRetorno.ERRO_GENERICO == bdResponse.CodigoRetorno)
            {
                response.IsExecucaoSucesso = false;
                response.ListaErros = new List<int>() { { (int)CodigoRetorno.ERRO_CADASTRO_PRODUTO } };
            }

            return response;
        }

        /// <summary>
        /// Método responsável por montar o CadastraProdutoRequest do banco de dados através do 
        /// CadastraProdutoRequest que chegou do aplicativo.
        /// </summary>
        /// <param name="request">
        /// Objeto do tipo CadastraProdutoRequest da Acao com as informações que 
        /// chegaram do aplicativo
        /// </param>
        /// <returns>
        /// CadastraProdutoRequest do banco de dados com as informações convertidas do 
        /// CadastraProdutoRequest da Acao.
        /// </returns>
        private static entities.BancoDeDados.CadastraProdutoRequest AjustaProdutoParaCadastro(CadastraProdutoRequest request)
        {
            return new entities.BancoDeDados.CadastraProdutoRequest()
            {
                Produto = request.Produto
            };
        }

        /// <summary>
        /// Método responsável por trocar o base 64 do arquivo de log 
        /// por "BASE64-> VAZIO" (se estiver em branco) ou "BASE64-> PREENCHIDO" (se estiver preenchido) 
        /// para não sobrecarregar o log.
        /// </summary>
        /// <param name="request">Objeto genérico IBaseAcaoRequest para buscar o base64.</param>
        protected override void GerarLogAcaoRequest(IBaseAcaoRequest request)
        {
            CadastraProdutoRequest cpRequest = request as CadastraProdutoRequest;
            if (cpRequest?.Produto != null)
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
