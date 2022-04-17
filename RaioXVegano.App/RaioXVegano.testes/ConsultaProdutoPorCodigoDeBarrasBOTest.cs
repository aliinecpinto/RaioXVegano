using NUnit.Framework;
using RaioXVegano.entities.App;
using RaioXVegano.entities.Enum;
using RaioXVegano.entities.MapMensagens;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;

namespace RaioXVegano.testes
{
    public class ConsultaProdutoPorCodigoDeBarrasBOTest : BaseBOTest
    {
        private IConsultaProdutoPorCodigoDeBarrasBO _bo;
        private ConsultaProdutoPorCodigoDeBarrasAppRequest _request;
        private MapCampoTelaMsgProduto _mapMensagens;

        public ConsultaProdutoPorCodigoDeBarrasBOTest() : base(typeof(ConsultaProdutoPorCodigoDeBarrasBOTest))
        {
            _bo = Container.GetInstance<IConsultaProdutoPorCodigoDeBarrasBO>();
            _mapMensagens = MapCampoTelaMsgProduto.Instancia;
        }

        private void PreencheRequestComSucessoProdutoVegano() 
        {
            _request = new ConsultaProdutoPorCodigoDeBarrasAppRequest() 
            { 
                ChaveUsuarioLogado = "ChaveUsuario1",
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_ALTERACAO_SUCESSO
            };
        }

        [SetUp]
        public void SetUp() 
        {
            PreencheRequestComSucessoProdutoVegano();
        }

        [Test]
        public void ErroChaveUsuarioLogadoEmBranco()
        {
            _request.ChaveUsuarioLogado = string.Empty;
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroCodigoBarrasEmBranco()
        {
            _request.CodigoDeBarras = string.Empty;
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroGenericoAoTentarConsultarProduto()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO;
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroInesperadoAoTentarConsultarProduto()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO;
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void SucessoConsultarProdutoQueEstaSendoEditado()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO;
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsProdutoSendoEditado);
        }

        [Test]
        public void SucessoAoConsultarProdutoVegano()
        {
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _bo.Executar(_request);

            Assert.IsNull(response.Mensagens);
            Assert.IsTrue(response.Produto.IsVegano);
        }

        [Test]
        public void SucessoAoConsultarProdutoNaoVegano()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO;
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _bo.Executar(_request);

            Assert.IsNull(response.Mensagens);
            Assert.IsTrue(!response.Produto.IsVegano);
        }

        [Test]
        public void SucessoProdutoNaoEncontrado()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_CADATRO_SUCESSO;
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _bo.Executar(_request);

            Assert.False(response.IsProdutoEncontrado);
        }
    }
}
