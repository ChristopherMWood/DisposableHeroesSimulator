using System;

namespace DisposableHeroes.Domain.Dice
{
    public class TwentySidedDice : BaseDice
    {
        public int Roll()
        {
            return Random.Next(1, 21);
        }
    }
}
