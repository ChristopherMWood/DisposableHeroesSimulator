namespace DisposableHeroes.Domain.Cards
{
    public class WeaponCard : ICard
    {
        public string Name { get; }
        public int Damage { get; }

        public WeaponCard(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }
    }
}
