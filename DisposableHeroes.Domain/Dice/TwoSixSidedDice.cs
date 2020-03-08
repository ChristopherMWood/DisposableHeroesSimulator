namespace DisposableHeroes.Domain.Dice
{
    public class TwoSixSidedDice : BaseDice
    {
        public int Roll()
        {
            return new SixSidedDice().Roll() + new SixSidedDice().Roll();
        }
    }
}
