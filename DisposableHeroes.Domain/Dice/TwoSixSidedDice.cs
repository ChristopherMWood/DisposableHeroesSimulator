namespace DisposableHeroes.Domain.Dice
{
    public class TwoSixSidedDice : BaseDice
    {
        public static int Roll()
        {
            return SixSidedDice.Roll() + SixSidedDice.Roll();
        }
    }
}
