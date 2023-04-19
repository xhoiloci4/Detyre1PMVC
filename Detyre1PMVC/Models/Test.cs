using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Detyre1PMVC.Models
{
    [Keyless]
    public class Test
    {
        public int Labels { get; set; }
        public int Data { get; set;}
    }
}
