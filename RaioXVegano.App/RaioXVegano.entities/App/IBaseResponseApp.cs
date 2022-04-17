using System.Collections.Generic;

namespace RaioXVegano.entities.App
{
    public interface IBaseResponseApp
    {
        IDictionary<string, string> Mensagens { get; set; }
    }
}
