using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PSI_FRONT.Models
{
    public class Cart
    {
        [JsonPropertyName("cartId")]
        public int CartId { get; set; }

        [JsonPropertyName("memberID")]
        public User User { get; set; }

        [JsonPropertyName("productID")]
        public Product Product { get; set; }

        [JsonPropertyName("quantity")]
        [Range(1, 100000, ErrorMessage = "Valor inválido - Selecione a quantidade entre 1 e 100000")]
        public int Quantity { get; set; }
    }
}
