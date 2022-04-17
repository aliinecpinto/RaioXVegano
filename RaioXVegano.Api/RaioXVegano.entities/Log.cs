using System;

namespace RaioXVegano.entities
{
    public class Log
    {
        public virtual int Id { get; set; }
        public virtual string ChaveUsuarioLogado { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual string Parametro1 { get; set; }
        public virtual string Parametro2 { get; set; }
    }
}
