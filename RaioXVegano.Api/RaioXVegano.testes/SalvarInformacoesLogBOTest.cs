using NUnit.Framework;
using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.testes
{
    public class SalvarInformacoesLogBOTest : BaseBOTest
    {

        private ISalvarInformacoesLogBO _bo;
        private SalvarInformacoesLogRequest _request;

        public SalvarInformacoesLogBOTest() : base(typeof(SalvarInformacoesLogBOTest)) 
        {
            _bo = Container.GetInstance<ISalvarInformacoesLogBO>();
        }

        [SetUp]
        public void Setup()
        {
            _request = new SalvarInformacoesLogRequest()
            {
                ChaveUsuarioLogado = Consts.TESTE_CODIGO_USUARIO_SUCESSO,
                Data = DateTime.Now,
                Parametro1 = "Parametro1",
                Parametro2 = "Parametro2"
            };
        }

        [Test]
        public void ErroRequestNull()
        {
            _request = null;
            SalvarInformacoesLogResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroChaveUsuarioLogadoNull()
        {
            _request.ChaveUsuarioLogado = null;
            SalvarInformacoesLogResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroDataSemPreenchimento()
        {
            _request.Data = DateTime.MinValue;
            SalvarInformacoesLogResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroParametro1EmBranco()
        {
            _request.Parametro1 = string.Empty;
            SalvarInformacoesLogResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroParametro2EmBranco()
        {
            _request.Parametro2 = string.Empty;
            SalvarInformacoesLogResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroGenericoAoTentarSalvarLog()
        {
            _request.ChaveUsuarioLogado = Consts.TESTE_CODIGO_USUARIO_ERRO;
            SalvarInformacoesLogResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_SALVAR_LOG);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void SucessoAoSalvarLog()
        {
            SalvarInformacoesLogResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsExecucaoSucesso);
        }
    }
}
