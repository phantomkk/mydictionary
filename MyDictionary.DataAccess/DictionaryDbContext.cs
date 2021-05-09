using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using MyDictionary.DataAccess.Context;
using MyDictionary.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.DataAccess
{
    public class DictionaryDbContext : DbContext
    {

        public DbSet<Word> Words { get; set; }
        public DbSet<Example> Examples{ get; set; }
        public DbSet<WordExample> WordExamples{ get; set; }
        public DbSet<User> Users{ get; set; }
        public DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
  
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory)  //tie-up DbContext with LoggerFactory object
                .EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Word>()
                .HasIndex(x => x.Name).IsUnique();


            modelBuilder.Entity<Example>()
                .Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(x => x.Id).ValueGeneratedOnAdd(); 

            modelBuilder.Entity<WordExample>()
                .HasKey(x => new { x.WordId, x.ExampleId });

            modelBuilder.Entity<WordExample>()
                .HasOne(x => x.Word)
                .WithMany(x => x.Examples)
                .HasForeignKey(x => x.WordId);

            modelBuilder.Entity<WordExample>()
                .HasOne(x => x.Example)
                .WithMany(x => x.Words)
                .HasForeignKey(x => x.ExampleId);

            Seeding.Seed(modelBuilder);
        }
    }
}
