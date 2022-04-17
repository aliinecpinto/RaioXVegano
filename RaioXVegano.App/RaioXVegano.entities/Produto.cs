namespace RaioXVegano.entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Nome { get; set; }
        public bool? IsVegano { get; set; }
        public string Motivo { get; set; }
        public string Ingredientes { get; set; }
        public virtual string UsuarioEditando { get; set; }
        public virtual string Base64ImagemProduto { get; set; }
    }
}
