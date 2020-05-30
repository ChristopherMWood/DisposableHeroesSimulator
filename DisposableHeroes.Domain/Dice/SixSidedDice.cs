using System;

namespace DisposableHeroes.Domain.Dice
{
    public class SixSidedDice : BaseDice
    {
        public int Roll()
        {
            return Random.Next(1, 7);
        }
    }
}
