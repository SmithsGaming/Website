namespace SmithsModding_Website.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewsModel : DbContext
    {
        public NewsModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<NewsItem> NewsItems { get; set; }

    }
}
