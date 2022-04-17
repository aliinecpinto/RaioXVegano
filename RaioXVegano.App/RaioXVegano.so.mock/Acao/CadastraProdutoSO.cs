using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;

namespace RaioXVegano.so.mock.Acao
{
    public class CadastraProdutoSO : BaseAcaoSO<CadastraProdutoRequest, CadastraProdutoResponse>, ICadastraProdutoSO
    {
        public CadastraProdutoSO() { }

        public override CadastraProdutoResponse Executa(CadastraProdutoRequest request)
        {
            CadastraProdutoResponse response = new CadastraProdutoResponse() { IsExecucaoSucesso = true };

            if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO.Equals(request.Produto.CodigoDeBarras))
            {
                throw new Exception();
            }
            else if (!Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_CADATRO_SUCESSO.Equals(request.Produto.CodigoDeBarras) &&
                !Consts.TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO.Equals(request.Produto.CodigoDeBarras))
            {
                response.IsExecucaoSucesso = false;
                response.ListaErros = new List<int>() { { (int)CodigoRetorno.ERRO_GENERICO } }; 
            }

            return response;
        }
    }
}
