namespace RaioXVegano.Util
{
    public static class Consts
    {
        /// <summary>
        /// Constante para iniciar o projeto da seguinte forma:
        /// 0 - Projeto não for rodado localmente ou para testes unitários.
        /// 1 - Projeto for rodado localmente
        /// </summary>
        public const string RUN_LOCAL = "run_local";

        /// <summary>
        /// Constante para iniciar o projeto da seguinte forma:
        /// 0 - Projeto já estiver rodando e não iniciar database do zero.
        /// 1 - Projeto for começar e sempre iniciar database do zero.
        /// </summary>
        public const string INICIAR_PROJETO = "iniciar_projeto";

        /// <summary>
        /// Constante para preencher a sessão com a url que será utilizada para conexão com a API.
        /// </summary>
        public const string URL = "url";

        /// <summary>
        /// Constante de true = 1
        /// </summary>
        public const string TRUE = "1";
        /// <summary>
        /// Constante de false = 0
        /// </summary>
        public const string FALSE = "0";

        /// <summary>
        /// Constante para colocar e buscar da sessão a chave do usuário.
        /// </summary>
        public const string SESSAO_CHAVE_USUARIO_LOGADO = "SessaoChaveUsuarioLogado";

        public const string SESSAO_CHAVE_CRIPTOGRAFIA = "SessaoChaveCriptografia";

        public const string SESSAO_IV_CRIPTOGRAFIA = "SessaoIVCriptografia";

        /// <summary>
        /// Chave do web.config para o caminho que ficará o arquivo de chave de criptografia
        /// </summary>
        public const string CAMINHO_CHAVE = "caminhoChave";

        /// <summary>
        /// Chave do web.config para o nome do arquivo de chave de criptografia
        /// </summary>
        public const string NOME_ARQUIVO_CHAVE = "nomeArquivoChave";

        /// <summary>
        /// Separador de strings definido para o projeto.
        /// </summary>
        public const string SEPARADOR = "_SEPARADOR_";

        /// <summary>
        /// Constante que define qual ambiente está rodando a API ou APP
        /// </summary>
        public const string AMBIENTE = "ambiente";

        /// <summary>
        /// Constante para definir ambiente de desenvolvimento
        /// </summary>
        public const string AMBIENTE_DEV = "DEV";

        /// <summary>
        /// Constante para definir ambiente de homologação
        /// </summary>
        public const string AMBIENTE_HML = "HML";

        /// <summary>
        /// Constante para definir ambiente de produção
        /// </summary>
        public const string AMBIENTE_PRD = "PRD";

        /// <summary>
        /// Campo Nome da Tela para mapeamento
        /// </summary>
        public const string NOME_PRODUTO = "Nome";

        /// <summary>
        /// Campo bool de vegano ou não da Tela para mapeamento
        /// </summary>
        public const string TIPO_PRODUTO = "Tipo";

        /// <summary>
        /// Campo Motivo da Tela para mapeamento
        /// </summary>
        public const string MOTIVO = "Motivo";

        /// <summary>
        /// Erro genérico para mapeamento
        /// </summary>
        public const string ERRO_GENERICO = "ErroGenerico";

        /// <summary>
        /// Erro produto sendo editado para mapeamento da tela
        /// </summary>
        public const string ERRO_PRODUTO_SENDO_EDITADO = "ErroProdutoSendoEditado";



        /// <summary>
        /// Código de barras para teste unitário de produto com erro genérico
        /// </summary>
        public const string TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO = "7899430100052";
        /// <summary>
        /// Código de barras para teste unitário de produto com erro de sendo editado
        /// </summary>
        public const string TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO = "7898931632017";
        /// <summary>
        /// Código de barras para teste unitário de produto vegano alterado com sucesso
        /// </summary>
        public const string TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_ALTERACAO_SUCESSO = "7898213360751";
        /// <summary>
        /// Código de barras para teste unitário de produto vegano cadastrado com sucesso
        /// </summary>
        public const string TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_CADATRO_SUCESSO = "7897517205614";
        /// <summary>
        /// Código de barras para teste unitário de produto não vegano
        /// </summary>
        public const string TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO = "7893000394209";
        
        /// <summary>
        /// Código de barras para teste unitário de erro não esperado
        /// </summary>
        public const string TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO = "1111111111111";

        /// <summary>
        /// Código de usuário para teste unitário de erro não esperado.
        /// </summary>
        public const string TESTE_CODIGO_USUARIO_ERRO = "USUARIO_1";

        /// <summary>
        /// Código de usuário para teste unitário de sucesso.
        /// </summary>
        public const string TESTE_CODIGO_USUARIO_SUCESSO = "USUARIO_0";

        /// <summary>
        /// Nome do arquivo zip do log.
        /// </summary>
        public const string NOME_ARQUIVO_LOG_ZIP = "Logs.zip";
        
        /// <summary>
        /// Nome da paste de logs do app.
        /// </summary>
        public const string CAMINHO_LOG = "Logs/";
    }
}
