namespace DisposableHeroes.Domain.Cards.BodyParts
{
    public interface IBodyPart
    {
        int Strength { get; }
        int Agility { get; }
        int Perception { get; }
    }
}
