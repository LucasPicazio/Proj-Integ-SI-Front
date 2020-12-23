using System.Text.Json.Serialization;

namespace PSI_FRONT.Models
{
    public class Transaction
    {
        [JsonPropertyName("transactionId")]
        public int TransactionId { get; set; }

        [JsonPropertyName("memberId")]
        public User User { get; set; }

        [JsonPropertyName("productId")]
        public Product Product { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
}
