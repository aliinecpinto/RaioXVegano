using RaioXVegano.di;
using RaioXVegano.entities;
using RaioXVegano.Util;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaioXVegano.testes
{
    public abstract class BaseBOTest
    {
        protected Container Container { get; }

        public BaseBOTest(Type type)
        {
            //Injeção de Dependência
            DependencyInjection.Configure();
            Container = DependencyInjection.Container;

            AplicacaoUtil.Ambiente = Consts.AMBIENTE_DEV;
        }

        protected Produto PreencheProdutoVeganoNovo()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_CADATRO_SUCESSO,
                Id = 1,
                Ingredientes = "Tudo com amor e sem crueldade",
                IsVegano = true,
                Nome = "Produto com Amor",
                Base64ImagemProduto = "base64"
            };
        }

        protected Produto PreencheProdutoVeganoParaAtualizacao()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_ALTERACAO_SUCESSO,
                Id = 1,
                Ingredientes = "Tudo com amor e sem crueldade",
                IsVegano = true,
                Nome = "Produto com Amor",
                Base64ImagemProduto = "base64"
            };
        }

        protected Produto PreencheProdutoNaoVegano()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO,
                Id = 2,
                Ingredientes = "Com crueldade =/",
                IsVegano = false,
                Nome = "Produto com Animais =/",
                Motivo = "Tem leite e ovos",
                Base64ImagemProduto = "base64"
            };
        }

        protected static bool VerificaSeCodigoFoiRetornado(IDictionary<string, string> mensagens, string campoTelaMapeado)
        {
            return mensagens.Keys.Contains(campoTelaMapeado);
        }
    }
}
