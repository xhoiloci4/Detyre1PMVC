using System.ComponentModel.DataAnnotations;

namespace Detyre1PMVC.Models
{
    public class Kategoria
    {
        [Key]
        public int? KategoriaId { get; set; }
        public string? Emri { get; set; }
        public  List<Libri> Libra { get; set; }
    }
}
