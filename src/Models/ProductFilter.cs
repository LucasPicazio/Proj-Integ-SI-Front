using System.ComponentModel.DataAnnotations;

namespace PSI_FRONT.Models
{
    public class ProductFilter
    {
        [Range(1, 99999, ErrorMessage = "Valor inválido")]
        public double MinPrice { get; set; }

        [Range(2, 100000, ErrorMessage = "Valor inválido")]
        public double MaxPrice { get; set; }
    }
}
