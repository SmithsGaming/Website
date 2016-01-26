namespace SmithsModding_Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NewsItem
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(256)]
        public string Post { get; set; }

        public DateTime PublishDate { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }
    }
}
