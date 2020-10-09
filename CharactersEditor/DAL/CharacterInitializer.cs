using CharactersEditor.Models;
using System.Collections.Generic;

namespace CharactersEditor.DAL
{
    public class CharacterInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CharacterContext>
    {
        protected override void Seed(CharacterContext context)
        {
            var characters = new List<Character>
            {
                new Character{Pseudonym = "Человек-паук", Kind = "Мутант", Position = "Добро", Universe = "Marvel"},
                new Character{Pseudonym = "Доктор Октопус", Kind = "Киборг", Position = "Зло", Universe = "Marvel"},
                new Character{Pseudonym="Капитан Америка", Kind = "Человек", Position = "Добро", Universe = "DC"},
                new Character{Pseudonym = "Дэдпул", Kind = "Мутант", Position = "Зло", Universe = "Marvel"},
                new Character{Pseudonym = "Джокер", Kind = "Человек", Position = "Зло", Universe = "DC"},
                new Character{Pseudonym = "Бэтмен", Kind = "Человек", Position = "Добро", Universe = "DC"},
                new Character{Pseudonym = "Тор", Kind = "Асгардец", Position = "Добро", Universe = "Marvel"},
                new Character{Pseudonym = "Флеш", Kind = "Человек", Position = "Добро", Universe = "DC"},
                new Character{Pseudonym = "Безумный шляпник", Kind = "Человек", Position = "Зло", Universe = "DC"},
                new Character{Pseudonym = "Росомаха", Kind = "Мутант", Position = "Добро", Universe = "Marvel"}
            };
            characters.ForEach(s => context.Characters.Add(s));
            context.SaveChanges();
        }
    }
}