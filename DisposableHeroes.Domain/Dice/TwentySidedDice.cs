using System;

namespace DisposableHeroes.Domain.Dice
{
    public class TwentySidedDice : BaseDice
    {
        public static int Roll()
        {
            return Random.Next(1, 21);
        }
    }
}
