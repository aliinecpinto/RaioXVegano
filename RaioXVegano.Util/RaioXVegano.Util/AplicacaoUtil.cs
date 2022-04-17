namespace RaioXVegano.Util
{
    public static class AplicacaoUtil
    {
        public static string Ambiente { get; set; }

        public static string GetDadosLog<T>(T obj) where T : class
        {
            string dados = ParseUtil.ParseJson(obj);
            if (!Consts.AMBIENTE_DEV.Equals(Ambiente) && !Consts.AMBIENTE_HML.Equals(Ambiente))
            {
                dados = CriptografiaUtil.Criptografar(dados);
            }

            return dados;
        }
    }
}
