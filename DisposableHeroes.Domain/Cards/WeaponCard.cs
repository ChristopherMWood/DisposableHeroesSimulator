using System.Linq;
using System.Text.Json;

namespace DisposableHeroes.Domain.Cards
{
    public class WeaponCard : ICard
    {
        public string Name { get; }
        public int Damage { get; }

        public WeaponType Type { get; }

        public WeaponCard(string name, int damage, WeaponType type)
        {
            Name = name;
            Damage = damage;
            Type = type;
        }

        public string Print()
        {
            return JsonSerializer.Serialize(this);
        }

        public string PrintReadable()
        {
            string output = @"[ ";

            foreach (var p in this.GetType().GetProperties())
            {
                if (p == this.GetType().GetProperties().Last())
                {
                    output += p.Name + ": " + p.GetValue(this) + " ";
                }
                else
                {
                    output += p.Name + ": " + p.GetValue(this) + ",\t";
                }
            }

            output += "]";

            return output;
        }
    }
}
