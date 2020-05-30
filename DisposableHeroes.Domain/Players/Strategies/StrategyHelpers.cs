using DisposableHeroes.Gameplay;
using System;
using System.Linq;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public class StrategyHelpers
    {
        protected static Random Random = new Random();

        public static Player ChooseRandomPlayer(Player playerToExclude, Game game)
        {
            var playersToChooseFrom = game.Players.Where(p => p != playerToExclude);
            var randomPlayerIndex = Random.Next(0, playersToChooseFrom.Count());

            return playersToChooseFrom.ElementAt(randomPlayerIndex);
        }
    }
}
