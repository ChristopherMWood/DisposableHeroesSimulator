using System;

namespace DisposableHeroes.Domain.Cards.BodyParts
{
    public class BodyPart : IBodyPart, ICard
    {
        public string Name { get; }
        public int Strength { get; }
        public int Agility { get; }
        public int Perception { get; }

        public BodyPart(string name, int strength, int agility, int perception)
        {
            Name = name;
            Strength = strength;
            Agility = agility;
            Perception = perception;
        }
    }
}
