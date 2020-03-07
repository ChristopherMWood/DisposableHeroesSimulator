namespace DisposableHeroes.Domain.Cards.BodyParts
{
    public class LegsCard : BodyPart, ICard
    {
        public LegsCard(string name, int strength, int agility, int perception) : base(name, strength, agility, perception)
        {

        }
    }
}
