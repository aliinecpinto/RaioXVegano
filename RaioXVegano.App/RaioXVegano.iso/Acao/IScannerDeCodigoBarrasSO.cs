using System.Threading.Tasks;

namespace RaioXVegano.iso.Acao
{
    public interface IScannerDeCodigoBarrasSO
    {
        Task<string> ScanAsync();
    }
}
