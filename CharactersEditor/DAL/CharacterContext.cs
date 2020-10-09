using CharactersEditor.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CharactersEditor.DAL
{
    public class CharacterContext : DbContext
    {
        public CharacterContext() : base("CharacterContext")
        {
        }

        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}