namespace SmithsModding_Website.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;

    public partial class NewsItem
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(512)]
        [DataType(DataType.MultilineText)]
        public string Post { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }
    }

    public partial class Project
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Message { get; set; }

        [Required]
        public string LogoPath { get; set; }

        [Required]
        public string PageContent { get; set; }

        public ICollection<DocumentationGroup> Documentation { get; set; }
    }

    public partial class DocumentationGroup
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string DisplayContents { get; set; }

        [Required]
        public DateTime LastEditOn { get; set; }

        [Required]
        public ApplicationUser LastEditor { get; set; }

        [Required]
        public Project Project { get; set; }

        public ICollection<DocumentationItem> Elements { get; set; }
    }

    public partial class DocumentationItem
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string DisplayContents { get; set; }

        [Required]
        public DateTime LastEditOn { get; set; }

        [Required]
        public ApplicationUser LastEditor { get; set; }

        [Required]
        public DocumentationGroup DocumentationGroup { get; set; }
    }



    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            context.Configuration.LazyLoadingEnabled = true;

            return context;
        }

        public virtual DbSet<NewsItem> News { get; set; }

        public virtual DbSet<Project> Projects { get; set; }
    }
}
