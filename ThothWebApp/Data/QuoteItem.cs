using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thoth.Data
{
    
    [Table("quotes")]
    public class QuoteItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public long Id {get; set;}
        [Column("primary_text")]
        public string PrimaryText{get; set;}
        [Column("secondary_text")]
        public string SecondaryText{get; set;}
        [Column("author")]
        public string Author {get; set;}
        [Column("post_time")]
        public DateTime PostTime{get; set;}
        [Column("is_visible")]
        public bool IsVisible {get; set;}

    }
}