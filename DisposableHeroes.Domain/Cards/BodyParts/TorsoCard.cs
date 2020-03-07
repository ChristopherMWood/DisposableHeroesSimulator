namespace DisposableHeroes.Domain.Cards.BodyParts
{
    public class TorsoCard : BodyPart, ICard
    {
        public TorsoCard(string name, int strength, int agility, int perception) : base(name, strength, agility, perception)
        {

        }
    }
}
