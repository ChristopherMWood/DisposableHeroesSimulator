using System;

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
    }
}
