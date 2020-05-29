using System.Linq;
using System.Text.Json;

namespace DisposableHeroes.Domain.Cards.BodyParts
{
    public class BodyPart : IBodyPart, ICard
    {
        public string Name { get; }
        public int Strength { get; }
        public int Agility { get; }
        public int Perception { get; }

        public BodyPart(string name, int strength, int agility, int perception)
        {
            Name = name;
            Strength = strength;
            Agility = agility;
            Perception = perception;
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
