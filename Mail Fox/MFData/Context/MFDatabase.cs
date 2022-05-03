using MFData.Entities;
using Microsoft.EntityFrameworkCore;

namespace MFData.Context
{
    internal sealed class MFDatabase : DbContext
    {
        private readonly string dataSource;

        public DbSet<UserEmail> UserEmails { get; private set; }

        public DbSet<Contact> Contacts { get; private set; }

        public DbSet<Blank> Blanks { get; private set; }

#pragma warning disable CS8618

        public MFDatabase(string dataSource) => this.dataSource = dataSource;

#pragma warning restore CS8618

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite($"Data Source={dataSource}");
    }
}