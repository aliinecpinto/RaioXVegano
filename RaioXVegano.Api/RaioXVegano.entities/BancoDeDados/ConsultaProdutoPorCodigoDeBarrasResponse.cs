using RaioXVegano.entities.Enum;

namespace RaioXVegano.entities.BancoDeDados
{
    public class ConsultaProdutoPorCodigoDeBarrasResponse : IBaseBancoDeDadosResponse
    {
        public CodigoRetorno CodigoRetorno { get; set; }
        public Produto Produto { get; set; }
    }
}
