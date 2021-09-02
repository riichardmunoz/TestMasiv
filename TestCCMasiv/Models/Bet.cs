using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestCCMasiv.Models
{
    [Serializable]
    public class Bet
    {
        public string Id { get; set; }
        [Range(minimum: 0, maximum: 36, ErrorMessage = "El número debe estar entre 0 y 36.")]
        public int Number { get; set; }
        public string Color { get; set; }
        [Range(minimum: 0, maximum: 10000, ErrorMessage = "La cantidad a apostar debe ser menor o igual a (USD)10.000")]
        public double Amount { get; set; }
        [Required]
        public string RouletteId { get; set; }
        public string IdUsuario { get; set; }
    }
}
