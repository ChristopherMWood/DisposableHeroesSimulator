namespace DisposableHeroes.Domain.Cards.BodyParts
{
    public class HeadCard : BodyPart, ICard
    {
        public HeadCard(string name, int strength, int agility, int perception) : base(name, strength, agility, perception)
        { 
        
        }
    }
}
