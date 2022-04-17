using NUnit.Framework;
using RaioXVegano.entities.App;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaioXVegano.testes
{
    public class SalvarInformacoesLogBOTest : BaseBOTest
    {
        private ISalvarInformacoesLogBO _bo;
        private SalvarInformacoesLogAppRequest _request;

        public SalvarInformacoesLogBOTest() : base(typeof(SalvarInformacoesLogBOTest)) 
        {
            _bo = Container.GetInstance<ISalvarInformacoesLogBO>();

        }

        [SetUp]
        public void SetUp() 
        {
            _request = new SalvarInformacoesLogAppRequest()
            {
                ChaveUsuarioLogado = Consts.TESTE_CODIGO_USUARIO_SUCESSO,
                Parametro1 = "parametro1",
                Parametro2 = "parametro2"
            };
        }

        [Test]
        public void ErroChaveUsuarioLogadoEmBranco()
        {
            _request.ChaveUsuarioLogado = string.Empty;
            SalvarInformacoesLogAppResponse response = _bo.Executar(_request);

            bool sucesso = response.Mensagens.Keys.Contains(Consts.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroParametro1EmBranco()
        {
            _request.Parametro1 = string.Empty;
            SalvarInformacoesLogAppResponse response = _bo.Executar(_request);

            bool sucesso = response.Mensagens.Keys.Contains(Consts.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroParametro2EmBranco()
        {
            _request.Parametro2 = string.Empty;
            SalvarInformacoesLogAppResponse response = _bo.Executar(_request);

            bool sucesso = response.Mensagens.Keys.Contains(Consts.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroGenerico()
        {
            _request.ChaveUsuarioLogado = Consts.TESTE_CODIGO_USUARIO_ERRO;
            SalvarInformacoesLogAppResponse response = _bo.Executar(_request);

            Assert.IsNotNull(response.Mensagens);
        }

        [Test]
        public void SucessoAoSalvarInformacoesLog()
        {
            SalvarInformacoesLogAppResponse response = _bo.Executar(_request);

            Assert.IsNull(response.Mensagens);
        }
    }
}
