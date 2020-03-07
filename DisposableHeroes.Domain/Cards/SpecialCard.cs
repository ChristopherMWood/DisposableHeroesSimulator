namespace DisposableHeroes.Domain.Cards
{
    public class SpecialCard : ICard
    {
        public SpecialType Type { get; }

        public SpecialCard(SpecialType type)
        {
            Type = type;
        }
    }
}
