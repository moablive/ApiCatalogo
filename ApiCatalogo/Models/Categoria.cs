using System.Text.Json.Serialization;

namespace ApiCatalogo.Models;

    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        //Relacionamento
        [JsonIgnore] //=>RELACIONAMENTO
        public ICollection<Produto>? Produtos { get; set; }
    }
