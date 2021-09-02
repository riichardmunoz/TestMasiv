using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCCMasiv.Models
{
    [Serializable]
    public class Roulette
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
