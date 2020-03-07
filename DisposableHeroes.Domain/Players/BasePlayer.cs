using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Domain.Player;

namespace DisposableHeroes.Domain.Players
{
    public class BasePlayer
    {
        public string Name { get; }
        public HeadCard Head { get; }
        public ArmsCard Arms { get; }
        public TorsoCard Torso { get; }
        public LegsCard Legs { get; }
        public Backpack Backpack { get; } = new Backpack();

        public BasePlayer(string name)
        {
            Name = name;
            Health = 25;
        }

        private int health = 20;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value > 25)
                    health = 25;
                else if (value < 0)
                    health = 0;
                else
                    health = value;
            }
        }
    }
}
