namespace DisposableHeroes.Domain.Cards
{
    public class WeaponCard : ICard
    {
        public string Name { get; }
        public int Damage { get; }

        public WeaponType Type { get; }

        public WeaponCard(string name, int damage, WeaponType type)
        {
            Name = name;
            Damage = damage;
            Type = type;
        }
    }
}
