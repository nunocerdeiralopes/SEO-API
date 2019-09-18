using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEO_API.Models
{
    public class RecurringKeyword
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecurringKeyworId { get; set; }
        [Required]
        public string Query { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string CountryDomain { get; set; }
    }
}
