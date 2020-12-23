using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PSI_FRONT.Models
{
    public class Product
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [JsonPropertyName("productType")]
        public string ProductType { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [JsonPropertyName("stock")]
        [Range(1, 1000, ErrorMessage = "O estoque deve variar entre 1 e 1000")]
        public int Stock { get; set; }

        [JsonPropertyName("price")]
        [Range(1, 100000, ErrorMessage = "O Preço deve variar entre 1 e 100.000")]
        public int Price { get; set; }

        [Url(ErrorMessage = "O link não é qualificado para o padrão http, https ou ftp")]
        [JsonPropertyName("imageSource")]
        public string ImageSource { get; set; }
    }
}
