using System.Linq;
using System.Text.Json;

namespace DisposableHeroes.Domain.Cards
{
    public class SpecialCard : ICard
    {
        public SpecialType Type { get; }

        public SpecialEffect Effect { get; }

        public SpecialCard(SpecialType type, SpecialEffect effect)
        {
            Type = type;
            Effect = effect;
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
