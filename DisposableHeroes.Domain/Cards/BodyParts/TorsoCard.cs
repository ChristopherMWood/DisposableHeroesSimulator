namespace DisposableHeroes.Domain.Cards.BodyParts
{
    public class TorsoCard : BodyPart, ICard
    {
        public int HealthBoost { get; private set; }

        public TorsoCard(string name, int strength, int agility, int perception, int healthBoost) : base(name, strength, agility, perception)
        {
            this.HealthBoost = healthBoost;
        }
    }
}
