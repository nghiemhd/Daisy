﻿using Daisy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Infrastructure
{
    public class DataContext : DbContext, IDbContext
    {
        static DataContext()
        {
            // Ignore using Code First Migrations to update the database
            Database.SetInitializer<DataContext>(null);
        }

        public DataContext() : base("name=DataContext") { }

        public DataContext(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Album>()
                .HasMany<Photo>(a => a.Photos)
                .WithMany(p => p.Albums)
                .Map(ap => {
                    ap.MapLeftKey("AlbumId");
                    ap.MapRightKey("PhotoId");
                    ap.ToTable("AlbumPhoto");
                });            

            modelBuilder.Entity<SliderPhoto>().HasKey(e => new { e.SliderId, e.PhotoId });
            modelBuilder.Entity<CategoryPhoto>().HasKey(e => new { e.CategoryId, e.PhotoId });
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sql, parameters);
        }

        #region DbSet
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<UrlRecord> UrlRecords { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<SliderPhoto> SliderPhotos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryPhoto> CategoryPhotos { get; set; }
        #endregion DbSet        
    }
}
