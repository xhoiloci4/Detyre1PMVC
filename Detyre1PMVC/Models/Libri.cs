using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Detyre1PMVC.Models
{
    public class Libri
    {
        [Key]
        public int? LibriId { get; set; }
        public string? Emri { get; set; }
        public string? Autori { get; set; }
        public DateTime DtPublikimi { get; set; }
        public int NrFaqe { get; set; }
        [ForeignKey("Kategoria")]
        public int KategoriId { get; set; }
        public virtual Kategoria? Kategoria { get; set; }
    }
}
