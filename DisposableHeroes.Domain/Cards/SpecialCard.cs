using System;
using System.Threading;

namespace DisposableHeroes.Domain.Cards
{
    public class SpecialCard : ICard
    {
        public SpecialType Type { get; }
        public SpecialEffect Effect { get; }
        public int HealthGain { get; private set; }
        public int TimesUsed { get; private set; } = 0;

        public SpecialCard(SpecialType type, SpecialEffect effect, int healthGain = 0)
        {
            Type = type;
            Effect = effect;
            HealthGain = healthGain;
        }

        public void Use()
        {
            TimesUsed++;
        }

        public void ResetToDefalt()
        {
            TimesUsed = 0;
        }
    }
}
