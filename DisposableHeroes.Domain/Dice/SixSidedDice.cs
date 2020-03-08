using System;

namespace DisposableHeroes.Domain.Dice
{
    public class SixSidedDice : BaseDice
    {
        public int Roll()
        {
            var random = new Random();
            return Random.Next(1, 7);
        }
    }
}
