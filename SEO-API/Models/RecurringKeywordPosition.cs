using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEO_API.Models
{
    public class RecurringKeywordPosition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecurringKeywordPositionId { get; set; }
        [Required]
        public int RecurringKeyworId { get; set; }
        [Required]
        public string Positions { get; set; }
        [Required]
        public DateTime date { get; set; }
    }
}
