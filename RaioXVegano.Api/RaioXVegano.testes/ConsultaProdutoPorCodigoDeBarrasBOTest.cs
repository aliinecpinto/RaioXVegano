using NUnit.Framework;
using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;

namespace RaioXVegano.testes
{
    public class ConsultaProdutoPorCodigoDeBarrasBOTest : BaseBOTest
    {
        private IConsultaProdutoPorCodigoDeBarrasBO _bo;
        private ConsultaProdutoPorCodigoDeBarrasRequest _request;

        public ConsultaProdutoPorCodigoDeBarrasBOTest() : base(typeof(ConsultaProdutoPorCodigoDeBarrasBOTest))
        {
            _bo = Container.GetInstance<IConsultaProdutoPorCodigoDeBarrasBO>();
        }

        private void PreencheRequestComSucessoProdutoVegano()
        {
            _request = new ConsultaProdutoPorCodigoDeBarrasRequest()
            {
                ChaveUsuarioLogado = "ChaveUsuario1",
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_ALTERACAO_SUCESSO
            };
        }

        [SetUp]
        public void Setup()
        {
            PreencheRequestComSucessoProdutoVegano();
        }

        [Test]
        public void ErroRequestNull()
        {
            _request = null;
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroChaveUsuarioLogadoNull()
        {
            _request.ChaveUsuarioLogado = null;
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroCodigoBarrasEmBranco()
        {
            _request.CodigoDeBarras = string.Empty;
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroGenericoAoTentarConsultarProduto()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO;
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_CONSULTAR_PRODUTO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroInesperadoAoTentarCadastrarProduto()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO;
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void SucessoConsultarProdutoQueEstaSendoEditado()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO;
            _request.ChaveUsuarioLogado = "ChaveUsuario2";
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.PRODUTO_SENDO_EDITADO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void SucessoAoConsultarProdutoQueEstaSendoEditadoParaMesmoUsuario()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO;
            _request.ChaveUsuarioLogado = "ChaveUsuario1";
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsExecucaoSucesso);
        }

        [Test]
        public void SucessoAoConsultarProdutoVegano()
        {
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsExecucaoSucesso);
            Assert.IsTrue(response.Produto.IsVegano);
        }

        [Test]
        public void SucessoAoConsultarProdutoNaoVegano()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO;
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsExecucaoSucesso);
            Assert.IsFalse(response.Produto.IsVegano);
        }

        [Test]
        public void SucessoProdutoNaoEncontrado()
        {
            _request.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_CADATRO_SUCESSO;
            ConsultaProdutoPorCodigoDeBarrasResponse response = _bo.Executar(_request);

            Assert.IsNull(response.Produto);
        }
    }
}