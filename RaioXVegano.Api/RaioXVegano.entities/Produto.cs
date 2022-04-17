using System;

namespace RaioXVegano.entities
{
    public class Produto
    {
        public virtual int Id { get; set; }
        public virtual string CodigoDeBarras { get; set; }
        public virtual string Nome { get; set; }
        public virtual bool IsVegano { get; set; }
        public virtual string Motivo { get; set; }
        public virtual string Ingredientes { get; set; }
        public virtual string UsuarioEditando { get; set; }
        public virtual string Base64ImagemProduto { get; set; }
        public virtual DateTime? DataAtualizacao { get; set; }
    }
}
