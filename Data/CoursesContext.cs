using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Courses.Models;
using System.IO;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Courses.Data
{
    public class CoursesContext : DbContext
    {
        public CoursesContext (DbContextOptions<CoursesContext> options)
            : base(options)
        {
        }

        public DbSet<Courses.Models.Category> Category { get; set; } = default!;

        public DbSet<Courses.Models.Content>? Content { get; set; }

        public DbSet<Courses.Models.CategoryItem>? CategoryItem { get; set; }

        public DbSet<Courses.Models.MediaType>? MediaType { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoryItem>()
            .HasOne<Category>(p => p.Category)
            .WithMany(p => p.CategoryItems)
            .HasForeignKey(p => p.CategoryId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<CategoryItem>()
            .HasMany<MediaType>(p => p.MediaTypes)
            .HasForeignKey(p => p.MediaTypeId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Content>()
            .HasOne<CategoryItem>(p => p.CategoryItem)
            .HasForeignKey(p => p.CatItemId);
            //.HasPrincipalKey(p => p.Id);
        }
    }
}
