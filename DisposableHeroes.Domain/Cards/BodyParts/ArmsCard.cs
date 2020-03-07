namespace DisposableHeroes.Domain.Cards.BodyParts
{
    public class ArmsCard : BodyPart, ICard
    {
        public ArmsCard(string name, int strength, int agility, int perception) : base(name, strength, agility, perception)
        {

        }
    }
}
